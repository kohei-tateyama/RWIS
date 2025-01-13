using UnityEngine;

public class NPCManager : MonoBehaviour
{
    private string NPCName;

    private void Awake() {
        NPCName = gameObject.name;
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
                if (StoryManager.Instance.day_of_the_week == "monday" && StoryManager.Instance.time_of_day == "morning")
                {
                    gameObject.SetActive(false);
                }
                else
                {
                    gameObject.SetActive(true);
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