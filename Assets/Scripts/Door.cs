using UnityEngine;

public class Door : MonoBehaviour
{

    public string roomToLoad;        // The name of the scene/room to load
    public string doorID;            // Unique identifier for this door
    public string targetDoorID;      // The doorID of the door in the target scene
    public bool isEntranceDoor;      // Indicates if this is an entrance door (used when returning)

    [Header("Glowing sprite")]
    [SerializeField] private GameObject glowSprite;
    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    public Color highlightColor = Color.yellow;
    public float blinkSpeed = 1f;

    private bool playerInRange;

    private void Awake() 
    {
        playerInRange = false;
        glowSprite.SetActive(false);
        spriteRenderer = glowSprite.GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
    }

    private void Update() 
    {
        if (playerInRange && !DialogueManager.Instance.dialogueIsPlaying) 
        {
            glowSprite.SetActive(true);

            // blink between original and highlight color
            float t = Mathf.PingPong(Time.time * blinkSpeed, 1f);
            spriteRenderer.color = Color.Lerp(originalColor, highlightColor, t);            
            
            if (InputManager.Instance.GetInteractPressed()) 
            {
                // Change scene / enter room
                GameManager.Instance.LoadRoom(roomToLoad);
            }
        }
        else 
        {
            glowSprite.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.tag == "Player")
        {
            playerInRange = true;

            if (isEntranceDoor)
            {
                // Store the player's current position before entering the door
                Debug.Log("player position registered: " + gameObject.transform.position);
                GameManager.Instance.previousPlayerPosition = gameObject.transform.position;
            }

            // Set the next door ID in the GameManager
            GameManager.Instance.nextDoorID = targetDoorID;
        }
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        if (other.gameObject.tag == "Player")
        {
            playerInRange = false;
        }
    }
}
