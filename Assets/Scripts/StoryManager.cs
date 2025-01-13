using UnityEngine;

public class StoryManager : MonoBehaviour
{
    public static StoryManager Instance { get; private set; }

    public string day_of_the_week; // Possible values: "monday", "tuesday", "wednesday", ...
    public string time_of_day;     // Possible values: "morning", "afternoon", "evening"
    public string class_session;   // Possible values: "before_class", "during_class", "after_class"

    private void Awake()
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        UpdateVariables();
    }

    private void UpdateVariables()
    {
        day_of_the_week = ((Ink.Runtime.StringValue) DialogueManager.Instance.GetVariableState("day_of_the_week")).value;
        time_of_day = ((Ink.Runtime.StringValue) DialogueManager.Instance.GetVariableState("time_of_day")).value;
        class_session = ((Ink.Runtime.StringValue) DialogueManager.Instance.GetVariableState("class_session")).value;
    }

    private void HandleVariableChange(string variableName, object newValue)
    {
        switch (variableName)
        {
            case "day_of_the_week":
                UpdateDayOfTheWeek(newValue.ToString());
                break;
            case "class_session":
                UpdateClassSession(newValue.ToString());
                break;
            case "lesson_completed":
                UpdateLessonCompleted((bool)newValue);
                break;
            default:
                break;
        }
    }

    private void UpdateDayOfTheWeek(string timeOfDay)
    {
        // Implement changes based on time of day
        // For example, adjust lighting, background music, etc.
    }

    private void UpdateClassSession(string classSession)
    {
        // Implement changes based on class session
        // Update NPC arrangements
        // NPCManager.Instance.UpdateClassArrangement(classSession);
    }

    private void UpdateLessonCompleted(bool lessonCompleted)
    {
        // Implement changes based on whether the lesson is completed
    }

}