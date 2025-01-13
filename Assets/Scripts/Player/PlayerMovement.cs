using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    private Camera mainCamera;
    private Rigidbody2D rb;
    private Collider2D capsuleCollider;

    private Vector2 velocity;
    private Vector2 inputAxis;
    
    private float maxJumpHeight = 5f;
    private float maxJumpTime = 1f;
    private float gravity => (-2f * maxJumpHeight) / Mathf.Pow(maxJumpTime / 2f, 2f);
    
    public float moveSpeed = 18f;
    
    public bool grounded { get; private set; }
    public bool running => Mathf.Abs(velocity.x) > 0.25f || Mathf.Abs(inputAxis.x) > 0.25f;
    
    private RectTransform canvasRectTransform;


    private void Awake()
    {
        mainCamera = Camera.main;
        rb = GetComponent<Rigidbody2D>();
        capsuleCollider = GetComponent<Collider2D>();
        
        Transform canvasTransform = transform.Find("Canvas");
        if (canvasTransform != null)
        {
            canvasRectTransform = canvasTransform.GetComponent<RectTransform>();
        }
        else
        {
            Debug.LogWarning("Canvas child GameObject not found under Player.");
        }
    }

    private void OnEnable()
    {
        rb.isKinematic = false;
        capsuleCollider.enabled = true;
        velocity = Vector2.zero;
    }

    private void OnDisable()
    {
        rb.isKinematic = true;
        capsuleCollider.enabled = false;
        velocity = Vector2.zero;
    }

    private void Update()
    {
        if (DialogueManager.Instance.dialogueIsPlaying || PauseMenuManager.GetInstance().isPauseMenuOn)
        {
            return;
        }
        
        HorizontalMovement();

        // grounded = rb.Raycast(Vector2.down);

        // if (grounded) {
        //     GroundedMovement();
        // }

        ApplyGravity();
    }

    private void FixedUpdate()
    {
        // Move mario based on his velocity
        Vector2 position = rb.position;
        position += velocity * Time.fixedDeltaTime;

        // Clamp within the screen bounds
        Vector2 leftEdge = mainCamera.ScreenToWorldPoint(Vector2.zero);
        Vector2 rightEdge = mainCamera.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        position.x = Mathf.Clamp(position.x, leftEdge.x + 0.5f, rightEdge.x - 0.5f);

        rb.MovePosition(position);
    }

    private void HorizontalMovement()
    {
        // Accelerate / decelerate
        inputAxis = InputManager.Instance.GetMoveDirection();
    
        // make player move at constant speed
        velocity.x = inputAxis.x * moveSpeed;

        // Check if running into a wall
        if (rb.Raycast(Vector2.right * velocity.x)) {
            velocity.x = 0f;
        }

        // Flip sprite to face direction
        if (velocity.x > 0f) 
        {
            transform.eulerAngles = Vector3.zero;
        } 
        else if (velocity.x < 0f) 
        {
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
        }

        // Avoid socail meter canvas to rotate with the player
        canvasRectTransform.eulerAngles = Vector3.zero;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("NPC") 
            || collision.gameObject.layer == LayerMask.NameToLayer("Door"))
        {
            InteractionButtonManager.GetInstance().ShowButton();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("NPC") 
            || collision.gameObject.layer == LayerMask.NameToLayer("Door"))
        {
            InteractionButtonManager.GetInstance().HideButton();
        }
    }

    private void ApplyGravity()
    {
        // Check if falling
        bool falling = velocity.y < 0f || !InputManager.Instance.GetJumpPressed();
        float multiplier = falling ? 2f : 1f;

        // Apply gravity and terminal velocity
        velocity.y += gravity * multiplier * Time.deltaTime;
        velocity.y = Mathf.Max(velocity.y, gravity / 2f);
    }

}
