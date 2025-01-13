using UnityEngine;

public class NPCManager : MonoBehaviour
{
    private string NPCName;

    private BoxCollider2D trigger;

    private AnimatedSprite animatedSprite;

    private DialogueTrigger dialogueTrigger;

    private bool playDialogue = true;


    private void Awake() {
        NPCName = gameObject.name;
        trigger = gameObject.GetComponentInChildren<BoxCollider2D>();
        dialogueTrigger = GetComponentInChildren<DialogueTrigger>();

        if (NPCName == "Miko")
        {
            animatedSprite = GetComponentInChildren<AnimatedSprite>();
        }
    }

    private void Update()
    {
        UpdateNPC();
    }

    private void UpdateNPC()
    {
        switch (NPCName)
        {
            case "Dad":
            {
                if (StoryManager.Instance.goal == "ask_dad_for_item" || StoryManager.Instance.goal == "tbc")
                {
                    foreach (Transform child in transform)
                    {
                        child.gameObject.SetActive(true);
                    }
                }
                else
                {
                    foreach (Transform child in transform)
                    {
                        child.gameObject.SetActive(false);
                    }
                }
                break;
            }
            case "Layla":
            {
                if (StoryManager.Instance.goal == "go_home" || StoryManager.Instance.goal == "ask_dad_for_item")
                {
                    foreach (Transform child in transform)
                    {
                        child.gameObject.SetActive(true);
                    }

                    if (dialogueTrigger.playerInRange && playDialogue)
                    {
                        playDialogue = false;
                        StartCoroutine(dialogueTrigger.AutomaticDialoguePlay());
                    }

                    if (StoryManager.Instance.goal != "go_home")
                    {
                        trigger.enabled = false;
                    }
                }
                else
                {
                    foreach (Transform child in transform)
                    {
                        child.gameObject.SetActive(false);
                    }
                }
                break;
            }
            case "Miko":
            {
                if (StoryManager.Instance.goal == "talk_to_Miko")
                {
                    trigger.enabled = true;
                    if (dialogueTrigger.playerInRange &&
                        DialogueManager.Instance.dialogueIsPlaying == true)
                    {
                        animatedSprite.enabled = true;
                    }
                }
                else
                {
                    animatedSprite.enabled = false;
                    trigger.enabled = false;
                }
                break;
            }
            case "Teacher":
            {
                if (StoryManager.Instance.goal == "go_to_class")
                {
                    trigger.enabled = true;
                }
                else
                {
                    trigger.enabled = false;
                }
                break;
            }
            case "HeadTeacher":
            {
                if (StoryManager.Instance.goal == "go_to_registration")
                {
                    trigger.enabled = true;
                }
                else
                {
                    trigger.enabled = false;
                }
                break;
            }
            case "Note":
            {
                if (StoryManager.Instance.goal == "spaceport")
                {
                    foreach (Transform child in transform)
                    {
                        child.gameObject.SetActive(true);
                    }
                }
                else
                {
                    foreach (Transform child in transform)
                    {
                        child.gameObject.SetActive(false);
                    }
                }
                break;
            }
            default:
            {
                break;
            }
        }
    }

}
