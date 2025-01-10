using UnityEngine;

public class Door : MonoBehaviour
{
    [Header("Target Position")]
    [SerializeField] private Transform targetPosition;

    [Header("Glowing sprite")]
    [SerializeField] private GameObject glowSprite;
    
    private SpriteRenderer spriteRenderer;
    public Transform connection;
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

            // Blink between original and highlight color
            float t = Mathf.PingPong(Time.time * blinkSpeed, 1f);
            spriteRenderer.color = Color.Lerp(originalColor, highlightColor, t);

            if (InputManager.Instance.GetInteractPressed())
            {
                // Move player or NPC to the target position
                MovePlayerToTargetPosition();
            }
        }
        else
        {
            glowSprite.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }

    private void MovePlayerToTargetPosition()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null && targetPosition != null)
        {
            // Move the player to the target position
            player.transform.position = targetPosition.position + new Vector3(0f, -0.4f, 0f);
            var sideSrolling = Camera.main.GetComponent<SideScrollingCamera>();
            sideSrolling.MoveCharacter(targetPosition.position.y);
        }
        else
        {
            Debug.LogWarning("Player or target position is missing!");
        }
    }
}
