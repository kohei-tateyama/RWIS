using UnityEngine;

public class Door : MonoBehaviour
{
    // [Header("Interact button")]
    // [SerializeField] private GameObject interactButton;

    [Header("Room number")]
    [SerializeField] private int roomNumber;

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
        // interactButton.SetActive(false);
    }

    private void Update() 
    {
        if (playerInRange && !DialogueManager.GetInstance().dialogueIsPlaying) 
        {
            glowSprite.SetActive(true);

            // blink between original and highlight color
            float t = Mathf.PingPong(Time.time * blinkSpeed, 1f);
            spriteRenderer.color = Color.Lerp(originalColor, highlightColor, t);

            // interactButton.SetActive(true);
            if (InputManager.GetInstance().GetInteractPressed()) 
            {
                // Change scene / enter room
                GameManager.Instance.LoadRoom(roomNumber);
            }
        }
        else 
        {
            glowSprite.SetActive(false);
            // interactButton.SetActive(false);
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
