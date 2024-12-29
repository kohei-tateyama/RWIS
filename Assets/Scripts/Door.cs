using UnityEngine;

public class Door : MonoBehaviour
{

    [Header("Room name")]
    [SerializeField] private string roomName;

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
                GameManager.Instance.LoadRoom(roomName);
            }
        }
        else 
        {
            glowSprite.SetActive(false);
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
