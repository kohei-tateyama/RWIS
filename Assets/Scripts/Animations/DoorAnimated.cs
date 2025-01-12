using System.Collections;
using UnityEngine;

public class DoorAnimated : MonoBehaviour
{
    [SerializeField] private GameObject touchInputPanel;
    [SerializeField] private AnimatedSpriteHome OtherDoor;
    [SerializeField] private AnimatedSpriteHome HallwayDoor;
    [SerializeField] private SideScrollingCamera sideScrollingCamera;
    
    [Header("Target Position")]
    [SerializeField] private Transform targetPosition;

    [Header("Glowing sprite")]
    [SerializeField] private GameObject glowSprite;
    
    private SpriteRenderer spriteRenderer;
    float animatedDuration = 0f;
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
    private IEnumerator WaitAndExit(float animatedDuration, AnimatedSpriteHome In)
    {
        
        yield return new WaitForSeconds(animatedDuration);
        MovePlayerToTargetPosition();

        // Animation of exiting the door
        In.frame = In.sprites.Length - 1;
        In.InvokeRepeating("ExitDoor", 0f, In.framerate);
        animatedDuration = In.sprites.Length * In.framerate;
        yield return new WaitForSeconds(animatedDuration);
        touchInputPanel.SetActive(true);

        // Execute the remaining actions after the animation
    }
    private void Enter(AnimatedSpriteHome In, AnimatedSpriteHome Out)
    {
        touchInputPanel.SetActive(false);

        // Animation of entering the door
        Out.frame = 0;
        Out.InvokeRepeating("EnterDoor", 0f, Out.framerate);
        animatedDuration = Out.sprites.Length * Out.framerate;
        StartCoroutine(WaitAndExit(animatedDuration, In));
    }

    public void Update()
    {
        if (playerInRange && !DialogueManager.Instance.dialogueIsPlaying)
        {
            // Blink between original and highlight color
            glowSprite.SetActive(true);
            float t = Mathf.PingPong(Time.time * blinkSpeed, 1f);
            spriteRenderer.color = Color.Lerp(originalColor, highlightColor, t);
            
            bool isInteractButtonPressed = InputManager.Instance.GetInteractPressed();
            if (isInteractButtonPressed && targetPosition.position.y > sideScrollingCamera.homeThreshold)
            {   
                Enter(OtherDoor, HallwayDoor);
            }
            else if (isInteractButtonPressed && targetPosition.position.y > sideScrollingCamera.teachersRoomThreshold)
            {
                Enter(OtherDoor, HallwayDoor);
            }
            else if (isInteractButtonPressed && targetPosition.position.y > sideScrollingCamera.classroomThreshold)
            {   
                Enter(HallwayDoor, OtherDoor);
            }
            else if (isInteractButtonPressed && targetPosition.position.y < sideScrollingCamera.classroomThreshold)
            {
                Enter(OtherDoor,HallwayDoor);
            }
            else if (isInteractButtonPressed)
            {
                Debug.Log("Room threshold not set");
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

    public void MovePlayerToTargetPosition()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null && targetPosition != null)
        {
            // Move the player to the target position
            player.transform.position = targetPosition.position + new Vector3(0f, -1.35f, 0f);
            
            // Move camera to follow player position
            var sideScrolling = Camera.main.GetComponent<SideScrollingCamera>();
            sideScrolling.MoveCamera(targetPosition.position);
        }
        else
        {
            Debug.LogWarning("Player or target position is missing!");
        }
    }
}
