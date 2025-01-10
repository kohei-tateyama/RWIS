using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private GameObject touchInputPanel;
    [SerializeField] private AnimatedSpriteHome animatedSpriteHome;
    [SerializeField] private SideScrollingCamera sideScrollingCamera;
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
    public bool shouldAnimate = false;
    public bool blockMovement = false;

    private void Awake()
    {
        playerInRange = false;
        glowSprite.SetActive(false);
        spriteRenderer = glowSprite.GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
    }

    public void Update()
    {
        if (playerInRange && !DialogueManager.Instance.dialogueIsPlaying)
        {
            glowSprite.SetActive(true);


            bool isInteractButtonPressed = InputManager.Instance.GetInteractPressed();


            // Blink between original and highlight color
            float t = Mathf.PingPong(Time.time * blinkSpeed, 1f);
            spriteRenderer.color = Color.Lerp(originalColor, highlightColor, t);
            if (isInteractButtonPressed && targetPosition.position.y > sideScrollingCamera.homeThreshold)
            {   
                Debug.Log("Enter Home");
                // Move player or NPC to the target position
                MovePlayerToTargetPosition();
                animatedSpriteHome.frame = animatedSpriteHome.sprites.Length - 1;
                animatedSpriteHome.InvokeRepeating("ExitDoor", 0f, animatedSpriteHome.framerate);
            }
            else if (isInteractButtonPressed && 
                    (targetPosition.position.y < sideScrollingCamera.teachersRoomThreshold && targetPosition.position.y > sideScrollingCamera.classroomThreshold))
            {
                Debug.Log("Enter Hallway");
                // Move player or NPC to the target position
                touchInputPanel.SetActive(false);
                animatedSpriteHome.frame = 0;
                animatedSpriteHome.InvokeRepeating("EnterDoor", 0f, animatedSpriteHome.framerate);
                // touchInputPanel.SetActive(true);

                // MovePlayerToTargetPosition();
            }
            else if (isInteractButtonPressed)
            {
                Debug.Log("Enter Classroom");
                MovePlayerToTargetPosition();
            }
            // if(InputManager.Instance.GetInteractPressed())
            // {
            //     MovePlayerToTargetPosition();
            // }
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

    public void MovePlayerToTargetPosition()
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
