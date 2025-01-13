using UnityEngine;

public class BoardingGate : MonoBehaviour{
    private bool playerInRange;

    private void Awake() 
    {
        playerInRange = false;
    }

    private void Update() 
    {
        if (StoryManager.Instance.goal == "go_to_gate" && 
            playerInRange && 
            !DialogueManager.Instance.dialogueIsPlaying) 
        {        
            if (InputManager.Instance.GetInteractPressed()) 
            {
                // make player spawn at home
                GameManager.Instance.LoadRoom("AllCombined", new Vector2(-20, 44));
            }
        }
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