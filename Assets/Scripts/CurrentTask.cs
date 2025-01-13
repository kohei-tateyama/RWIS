using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CurrentTask : MonoBehaviour
{
    // Reference to the TextMeshPro component
    [SerializeField] private TextMeshProUGUI textMeshPro;


    private void Update()
    {
        if (StoryManager.Instance.goal == "spaceport" && SceneManager.GetActiveScene().name == "AllCombined")
        {
            SetText("Look for dad");
        }
        else if (StoryManager.Instance.goal == "spaceport")
        {
            SetText("Talk to dad");
        }
        else if (StoryManager.Instance.goal == "go_to_gate")
        {
            SetText("*Go to the correct gate");
        }
        else if (StoryManager.Instance.goal == "go_to_registration") 
        {
            SetText("* Go to the Dean's office to register");
        }
        else if (StoryManager.Instance.goal == "go_to_class")
        {
            SetText("Go to the Classroom");
        }
        else if (StoryManager.Instance.goal == "talk_to_miko")
        {
            SetText("Talk with your Classmates");
        }
        else if (StoryManager.Instance.goal == "go_home")
        {
            SetText("Go Home");
        }
        else if (StoryManager.Instance.goal == "ask_dad_for_item")
        {
            SetText("Ask dad about baking stuff");
        }
        else
        {
            SetText("");
        }
        
    }

    // Method to set the text dynamically
    public void SetText(string newText)
    {
        if (textMeshPro != null)
        {
            textMeshPro.text = newText;
        }
        else
        {
            Debug.LogWarning("TextMeshProUGUI component is not assigned!");
        }
    }
}
