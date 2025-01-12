using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EavesDroppingTrigger : MonoBehaviour
{
    [Header("Visual Cue")]
    [SerializeField] private GameObject visualCue; // Visual indicator for the player
    
    [Header("Progress Text")]
    [SerializeField] private  TextMeshProUGUI progressText; //Text to display progress (e.g., "Listening: 50%")
    
    [Header("Progress Bar")]
    [SerializeField] private GameObject progressBarObject; // Progress bar or indicator for eavesdropping
    
    [Header("INK json")]
    [SerializeField] private TextAsset inkJSON;
    
    [Header("Listening Time")]
    [SerializeField] private float listeningTime = 4f;   // Time required to listen before unlocking dialogue
    

    private bool playerInRange;
    private bool isListening = false;
    private bool isDialogueUnlocked = false;
    private float currentListeningTime = 0f;

    private Slider progressBarSlider;

    private void Start()
    {
        if (progressBarObject != null)
        {
            progressBarSlider = progressBarObject.GetComponentInChildren<Slider>();
        }
    }
    private void Update()
    {
        // Main logic making sure that you need to listen for a while to get the whole context
        // Otherwise resets.
        if (playerInRange && !DialogueManager.Instance.dialogueIsPlaying)
        {
            if (!isDialogueUnlocked)
            {
                if (!isListening)
                {
                    visualCue.SetActive(true);
                    if (InputManager.Instance.GetInteractHeld())
                    {
                        StartListening();
                    }
                }
                else
                {
                    if (InputManager.Instance.GetInteractHeld())
                    { 
                        ContinueListening();
                    }
                    else
                    {
                        ResetListening();
                    }
                }
            }
            else
            {
                if (InputManager.Instance.GetInteractPressed())
                {
                    DialogueManager.Instance.EnterDialogueMode(inkJSON);
                }
            }
        }
        else
        {
            visualCue.SetActive(false);
            if (!isDialogueUnlocked)
            {
                ResetListening();
            }
        }
    }

    // Show the progress bar
    private void StartListening()
    {
        isListening = true;
        progressBarObject.SetActive(true); 
        currentListeningTime = 0f;
    }

    private void ContinueListening()
    {
        currentListeningTime += Time.deltaTime;
        UpdateProgressBar();

        if (currentListeningTime >= listeningTime)
        {
            UnlockDialogue();
        }
    }

    private void ResetListening()
    {
        isListening = false;
        currentListeningTime = 0f;
        progressBarObject.SetActive(false);
        UpdateProgressBar();
    }

    private void UpdateProgressBar()
    {
        float progress = Mathf.Clamp01(currentListeningTime / listeningTime);
        if (progressBarObject != null)
        {
            progressBarSlider.value = progress; 

        }
        if (progressText != null)
        {
            progressText.text = $"Listening: {Mathf.FloorToInt(progress * 100)}%";
        }
    }

    private void UnlockDialogue()
    {
        isListening = false;
        visualCue.SetActive(false);
        progressBarObject.SetActive(false);
        isDialogueUnlocked = true;
        DialogueManager.Instance.EnterDialogueMode(inkJSON);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            playerInRange = false;
        }
    }
}