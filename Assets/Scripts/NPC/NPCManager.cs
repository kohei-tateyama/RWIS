using UnityEngine;

public class NPCManager : MonoBehaviour
{
    private string NPCName;

    private BoxCollider2D trigger;

    private void Awake() {
        NPCName = gameObject.name;
        trigger = gameObject.GetComponentInChildren<BoxCollider2D>();
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
                if (StoryManager.Instance.day_of_the_week == "monday" && 
                    StoryManager.Instance.time_of_day == "morning")
                {
                    gameObject.SetActive(false);
                }
                else if (StoryManager.Instance.day_of_the_week == "monday" && 
                         StoryManager.Instance.time_of_day == "evening")
                {
                    gameObject.SetActive(true);
                }
                break;
            }
            case "Miko":
            {
                if (StoryManager.Instance.goal == "talk_to_miko")
                {
                    trigger.enabled = true;
                }
                else
                {
                    trigger.enabled = false;
                }
                break;
            }
            case "Teacher":
            {
                if (StoryManager.Instance.goal == "talk to teacher")
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
                if (StoryManager.Instance.day_of_the_week == "monday" && 
                    StoryManager.Instance.time_of_day == "morning")
                {
                    gameObject.SetActive(true);
                }
                else
                {
                    gameObject.SetActive(false);
                }
                break;
            }
            default:
            {
                Debug.LogWarning("NPC name case not handled! Check again");
                break;
            }
        }
    }

}
