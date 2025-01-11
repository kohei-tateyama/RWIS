using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardingGate : MonoBehaviour{
    private SpriteRenderer spriteRenderer;
    private bool playerInRange;

    private void Awake() 
    {
        playerInRange = false;
    }

    private void Update() 
    {
        if (playerInRange && !DialogueManager.Instance.dialogueIsPlaying) 
        {        
            if (InputManager.Instance.GetInteractPressed()) 
            {
                // Change scene / enter room
                GameManager.Instance.LoadRoom("AllCombined");
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