using System.Collections;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [Header("Visual Cue")]
    [SerializeField] private GameObject visualCue;

    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON;

    public bool playerInRange {get; private set;}

    private void Awake() 
    {
        playerInRange = false;
        visualCue.SetActive(false);
    }

    private void Update()
    {
        if (playerInRange && !DialogueManager.Instance.dialogueIsPlaying) 
        {
            visualCue.SetActive(true);
            if (InputManager.Instance.GetInteractPressed()) 
            {
                DialogueManager.Instance.EnterDialogueMode(inkJSON);
            }
        }
        else
        {
            visualCue.SetActive(false);
        }
    }

    public IEnumerator AutomaticDialoguePlay()
    {
        yield return new WaitForEndOfFrame();
        DialogueManager.Instance.EnterDialogueMode(inkJSON);
    }

    private void OnTriggerEnter2D(Collider2D collider) 
    {
        if (collider.gameObject.tag == "Player")
        {
            playerInRange = true;
            StartCoroutine(collider.gameObject.GetComponent<PlayerMovement>().SetVelocityToZero());
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
