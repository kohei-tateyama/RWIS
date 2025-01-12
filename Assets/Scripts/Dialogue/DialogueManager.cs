using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ink.Runtime;
using UnityEngine.EventSystems;
using System;
using UnityEngine.Audio;

public class DialogueManager : MonoBehaviour
{
    [Header("Params")]
    [SerializeField] private float typingSpeed = 0.04f;

    [Header("Load Globals JSON")]
    [SerializeField] private TextAsset loadGlobalsJSON;

    [Header("Dialogue UI")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private GameObject continueIcon;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private TextMeshProUGUI displayNameText;
    [SerializeField] private Animator portraitAnimator;
    private Animator layoutAnimator;

    [Header("Choices UI")]
    [SerializeField] private GameObject[] choices;
    private TextMeshProUGUI[] choicesText;

    [Header("Input Panel")]
    [SerializeField] private GameObject inputPanel;
    [SerializeField] private TMP_InputField nameInputField;

    [Header("Touch Input Panel")]
    [SerializeField] private GameObject touchInputPanel;
    private bool inputRequired;

    public int socialMeterValue { get; private set; }
    public int isFollowingMC {get; private set; }
    public event Action<int> OnSocialValueChangedEvent;

    [Header("Mixer Group - Dialogues")]
    [SerializeField] private AudioMixerGroup MixerGroupDialogue;
    public event Action<int> OnFollowingMCEvent;

    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;



    
    private Story currentStory;
    public bool dialogueIsPlaying { get; private set; }

    private bool canContinueToNextLine = false;

    private Coroutine displayLineCoroutine;

    private const string SPEAKER_TAG = "speaker";
    private const string PORTRAIT_TAG = "portrait";
    private const string LAYOUT_TAG = "layout";
    private const string AUDIO_TAG = "audio";
    private const string INPUT_REQ_TAG = "input_required";

    private DialogueVariables dialogueVariables;
    private InkExternalFunctions inkExternalFunctions;

    public static DialogueManager Instance { get; private set; }

    private void Awake() 
    {
        Instance = this;

        dialogueVariables = new DialogueVariables(loadGlobalsJSON);
        inkExternalFunctions = new InkExternalFunctions();

        // audioSource = this.gameObject.AddComponent<AudioSource>();
        // currentAudioInfo = defaultAudioInfo;

        if (touchInputPanel == null)
        {
            Debug.LogError("'DialogueManager': No Touch Input Panel associated within inspector");
        } 

        if (audioSource == null)
        {
            audioSource = this.gameObject.AddComponent<AudioSource>();
        }
    }

    private void Start() 
    {
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
        inputPanel.SetActive(false);
        inputRequired = false;

        // get the layout animator
        layoutAnimator = dialoguePanel.GetComponent<Animator>();

        // get all of the choices text 
        choicesText = new TextMeshProUGUI[choices.Length];
        int index = 0;
        foreach (GameObject choice in choices) 
        {
            choicesText[index] = choice.GetComponentInChildren<TextMeshProUGUI>();
            index++;
        }

        //InitializeAudioInfoDictionary();
    }

    //private void InitializeAudioInfoDictionary()
    //{
    //    audioInfoDictionary = new Dictionary<string, DialogueAudioInfoSO>();
    //    audioInfoDictionary.Add(defaultAudioInfo.id, defaultAudioInfo);
    //    foreach (DialogueAudioInfoSO audioInfo in audioInfos)
    //    {
    //        audioInfoDictionary.Add(audioInfo.id, audioInfo);
    //    }
    //}

    //private void SetCurrentAudioInfo(string id)
    //{
    //    DialogueAudioInfoSO audioInfo = null;
    //    audioInfoDictionary.TryGetValue(id, out audioInfo);
    //    if (audioInfo != null)
    //    {
    //        this.currentAudioInfo = audioInfo;
    //    }
    //    else
    //    {
    //        Debug.LogWarning("Failed to find audio info for id: " + id);
    //    }
    //}

    private void Update() 
    {
        // return right away if dialogue isn't playing
        if (!dialogueIsPlaying) 
        {
            return;
        }

        // handle continuing to the next line in the dialogue when submit is pressed
        // NOTE: The 'currentStory.currentChoiecs.Count == 0' part was to fix a bug after the Youtube video was made
        if (canContinueToNextLine 
            && currentStory.currentChoices.Count == 0 
            && InputManager.Instance.GetSubmitPressed())
        {
            ContinueStory();
        }
    }

    public void EnterDialogueMode(TextAsset inkJSON, Animator emoteAnimator) 
    {
        currentStory = new Story(inkJSON.text);
        dialogueIsPlaying = true;
        touchInputPanel.SetActive(false);
        dialoguePanel.SetActive(true);


        dialogueVariables.StartListening(currentStory);
        inkExternalFunctions.Bind(currentStory, emoteAnimator);

        // reset portrait, layout, and speaker
        displayNameText.text = "???";
        portraitAnimator.Play("default");
        layoutAnimator.Play("right");

        // Observe changes to 'social_meter'
        currentStory.ObserveVariable("social_meter", OnSocialVariableChanged);
        currentStory.BindExternalFunction("pause", (float seconds) => { StartCoroutine(PauseCoroutine(seconds));});
        currentStory.ObserveVariable("isGoingToGate", OnFollowingMC);

        ContinueStory();
    }

    private IEnumerator PauseCoroutine(float seconds)
    {
        yield return new WaitForSeconds(seconds);
    }

    private IEnumerator ExitDialogueMode() 
    {
        yield return new WaitForSeconds(0.2f);

        dialogueVariables.StopListening(currentStory);
        inkExternalFunctions.Unbind(currentStory);

        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
        dialogueText.text = "";

        currentStory.RemoveVariableObserver(OnSocialVariableChanged, "social_meter");
        currentStory.RemoveVariableObserver(OnFollowingMC, "isGoingToGate");

        // go back to default audio
        //SetCurrentAudioInfo(defaultAudioInfo.id);

        // Allow for player movements input
        touchInputPanel.SetActive(true);
    }

    private void ContinueStory() 
    {
        if (currentStory.canContinue) 
        {
            // set text for the current dialogue line
            if (displayLineCoroutine != null) 
            {
                StopCoroutine(displayLineCoroutine);
            }
            
            string nextLine = currentStory.Continue();
            
            // handle case where the last line is an external function
            if (nextLine.Equals("") && !currentStory.canContinue)
            {
                StartCoroutine(ExitDialogueMode());
            }
            // otherwise, handle the normal case for continuing the story
            else 
            {
                // handle tags
                HandleTags(currentStory.currentTags);
                displayLineCoroutine = StartCoroutine(DisplayLine(nextLine));
            }
        }
        else 
        {
            StartCoroutine(ExitDialogueMode());
        }
    }

    private IEnumerator DisplayLine(string line) 
    {
        // set the text to the full line, but set the visible characters to 0
        dialogueText.text = line;
        dialogueText.maxVisibleCharacters = 0;
        // hide items while text is typing
        continueIcon.SetActive(false);
        HideChoices();

        canContinueToNextLine = false;

        bool isAddingRichTextTag = false;

        // display each letter one at a time
        foreach (char letter in line.ToCharArray())
        {
            // if the submit button is pressed, finish up displaying the line right away
            if (InputManager.Instance.GetSubmitPressed()) 
            {
                dialogueText.maxVisibleCharacters = line.Length;
                break;
            }

            // check for rich text tag, if found, add it without waiting
            if (letter == '<' || isAddingRichTextTag) 
            {
                isAddingRichTextTag = true;
                if (letter == '>')
                {
                    isAddingRichTextTag = false;
                }
            }
            // if not rich text, add the next letter and wait a small time
            else 
            {
                //PlayDialogueSound(dialogueText.maxVisibleCharacters, dialogueText.text[dialogueText.maxVisibleCharacters]);
                dialogueText.maxVisibleCharacters++;
                //if (stopAudioSource)
                //{
                //    audioSource.Stop();
                //}
                //audioSource.PlayOneShot(dialogueTypingSoundClip);
                yield return new WaitForSeconds(typingSpeed);
            }
        }

        // actions to take after the entire line has finished displaying
        continueIcon.SetActive(true);
        DisplayChoices();
        DisplayInputField();

        canContinueToNextLine = true;
    }

    private void PlayDialogueAudio(string clipName)
    {
        // set variables for the below based on our config
        AudioClip clip = Resources.Load<AudioClip>($"{clipName}");
        if (clip != null)
        {
            audioSource.outputAudioMixerGroup = MixerGroupDialogue;
            audioSource.PlayOneShot(clip);
        }
        else
        {
            Debug.LogWarning("Audio clip not found: " + clipName);
        }
    }

    private void HideChoices() 
    {
        foreach (GameObject choiceButton in choices) 
        {
            choiceButton.SetActive(false);
        }
    }

    private void HandleTags(List<string> currentTags)
    {
        // loop through each tag and handle it accordingly
        foreach (string tag in currentTags) 
        {
            // parse the tag
            string[] splitTag = tag.Split(':');
            if (splitTag.Length != 2) 
            {
                Debug.LogError("Tag could not be appropriately parsed: " + tag);
            }
            string tagKey = splitTag[0].Trim();
            string tagValue = splitTag[1].Trim();
            
            // handle the tag
            switch (tagKey) 
            {
                case SPEAKER_TAG:
                    displayNameText.text = tagValue;
                    break;
                case PORTRAIT_TAG:
                    portraitAnimator.Play(tagValue);
                    break;
                case LAYOUT_TAG:
                    layoutAnimator.Play(tagValue);
                    break;
                case AUDIO_TAG:
                    PlayDialogueAudio(tagValue);
                    break;
                case INPUT_REQ_TAG:
                    inputRequired = true;
                    break;
                default:
                    Debug.LogWarning("Tag came in but is not currently being handled: " + tag);
                    break;
            }
        }
    }

    private void DisplayInputField()
    {
        if (inputRequired)
        {
            inputPanel.SetActive(true);
            StartCoroutine(ActivateInputField());
        }
    }

    private IEnumerator ActivateInputField()
    {
        // Wait until the end of the frame
        yield return new WaitForEndOfFrame();

        // Activate the input field
        nameInputField.ActivateInputField();
    }

    public void OnInputSubmitted()
    {
        // Get input from the player
        string playerInput = nameInputField.text;

        // Assign it back to the Ink story
        currentStory.variablesState["player_input"] = playerInput;

        // Deactivate and clear the input field and panel
        nameInputField.DeactivateInputField();
        nameInputField.text = "";
        inputPanel.SetActive(false);
        inputRequired = false;

        // Continue the story
        ContinueStory();
    }

    private void DisplayChoices() 
    {
        List<Choice> currentChoices = currentStory.currentChoices;

        // defensive check to make sure our UI can support the number of choices coming in
        if (currentChoices.Count > choices.Length)
        {
            Debug.LogError("More choices were given than the UI can support. Number of choices given: " 
                + currentChoices.Count);
        }

        int index = 0;
        // enable and initialize the choices up to the amount of choices for this line of dialogue
        foreach(Choice choice in currentChoices) 
        {
            choices[index].gameObject.SetActive(true);
            choicesText[index].text = choice.text;
            index++;
        }
        // go through the remaining choices the UI supports and make sure they're hidden
        for (int i = index; i < choices.Length; i++) 
        {
            choices[i].gameObject.SetActive(false);
        }

        StartCoroutine(SelectGameObject(choices[0].gameObject));
    }

    private IEnumerator SelectGameObject(GameObject gameObject)
    {
        // Event System requires we clear it first, then wait
        // for at least one frame before we set the current selected object.
        EventSystem.current.SetSelectedGameObject(null);
        yield return new WaitForEndOfFrame();
        EventSystem.current.SetSelectedGameObject(gameObject);
    }

    public void MakeChoice(int choiceIndex)
    {
        if (canContinueToNextLine) 
        {
            currentStory.ChooseChoiceIndex(choiceIndex);
            // NOTE: The below two lines were added to fix a bug after the Youtube video was made
            InputManager.Instance.RegisterSubmitPressed(); // this is specific to my InputManager script
            ContinueStory();
        }
    }

    private void OnFollowingMC(string variableName, object newValue)
    {
        isFollowingMC = Convert.ToInt32(newValue);
        Debug.Log("Following MC: " + isFollowingMC);
        //Notify other scripts
        OnFollowingMCEvent?.Invoke(isFollowingMC);
    }
    private void OnSocialVariableChanged(string variableName, object newValue)
    {
        socialMeterValue = Convert.ToInt32(newValue);
        
        // Notify other scripts
        OnSocialValueChangedEvent?.Invoke(socialMeterValue);
    }

    public Ink.Runtime.Object GetVariableState(string variableName) 
    {
        Ink.Runtime.Object variableValue = null;
        dialogueVariables.variables.TryGetValue(variableName, out variableValue);
        if (variableValue == null) 
        {
            Debug.LogWarning("Ink Variable was found to be null: " + variableName);
        }
        return variableValue;
    }

    // This method will get called anytime the application exits.
    // Depending on your game, you may want to save variable state in other places.
    public void OnApplicationQuit() 
    {
        dialogueVariables.SaveVariables();
    }

}
