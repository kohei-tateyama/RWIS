using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    private Camera mainCamera;
    private Rigidbody2D rb;
    private Collider2D capsuleCollider;

    private Vector2 velocity;
    private Vector2 inputAxis;
    private bool jumpPressed;

    public float moveSpeed = 8f;
    public float maxJumpHeight = 5f;
    public float maxJumpTime = 1f;
    public float jumpForce => (2f * maxJumpHeight) / (maxJumpTime / 2f);
    public float gravity => (-2f * maxJumpHeight) / Mathf.Pow(maxJumpTime / 2f, 2f);

    public bool grounded { get; private set; }
    public bool jumping { get; private set; }
    public bool running => Mathf.Abs(velocity.x) > 0.25f || Mathf.Abs(inputAxis.x) > 0.25f;
    public bool sliding => (inputAxis.x > 0f && velocity.x < 0f) || (inputAxis.x < 0f && velocity.x > 0f);
    public bool falling => velocity.y < 0f && !grounded;
    private RectTransform rectTransformSocialMeter,rectTransformBatteryBar;

    private void Awake()
    {
        mainCamera = Camera.main;
        rb = GetComponent<Rigidbody2D>();
        capsuleCollider = GetComponent<Collider2D>();
        Transform childObjectBattery = transform.Find("Canvas Battery");
        Transform childObjectSocialMeter = transform.Find("Canvas SM");
        rectTransformBatteryBar = childObjectBattery.GetComponent<RectTransform>();
        rectTransformSocialMeter = childObjectSocialMeter.GetComponent<RectTransform>();
    }

    private void OnEnable()
    {
        rb.isKinematic = false;
        capsuleCollider.enabled = true;
        velocity = Vector2.zero;
        jumping = false;
    }

    private void OnDisable()
    {
        rb.isKinematic = true;
        capsuleCollider.enabled = false;
        velocity = Vector2.zero;
        jumping = false;
    }

    private void Update()
    {
        if (DialogueManager.GetInstance().dialogueIsPlaying)
        {
            return;
        }
        
        HorizontalMovement();

        grounded = rb.Raycast(Vector2.down);

        if (grounded) {
            GroundedMovement();
        }

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
        // inputAxis = Input.GetAxis("Horizontal");
        inputAxis = InputManager.GetInstance().GetMoveDirection();
        
        // make player move with acceleration and deceleration periods
        // velocity.x = Mathf.MoveTowards(velocity.x, inputAxis.x * moveSpeed, moveSpeed * Time.deltaTime);
        
        // make player move at constant speed
        velocity.x = inputAxis.x * moveSpeed;

        // Check if running into a wall
        if (rb.Raycast(Vector2.right * velocity.x)) {
            velocity.x = 0f;
        }

        // Flip sprite to face direction
        if (velocity.x > 0f) {
            transform.eulerAngles = Vector3.zero;
            rectTransformBatteryBar.eulerAngles = Vector3.zero;
            rectTransformSocialMeter.eulerAngles = Vector3.zero;
        } else if (velocity.x < 0f) {
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
            rectTransformBatteryBar.eulerAngles = Vector3.zero;
            rectTransformSocialMeter.eulerAngles = Vector3.zero;

        }
    }

    private void GroundedMovement()
    {
        // Prevent gravity from infinitly building up
        velocity.y = Mathf.Max(velocity.y, 0f);
        jumping = velocity.y > 0f;

        // Perform jump
        // if (Input.GetButtonDown("Jump"))
        jumpPressed = InputManager.GetInstance().GetJumpPressed();
        
        // TODO: at the moment the jump height is fixed, if we want to change it to make it higher
        // according to how long player keeps the jump key hold down, we have to implement it
        if (jumpPressed)
        {
            velocity.y = jumpForce;
            jumping = true;
        }
    }

    private void ApplyGravity()
    {
        // Check if falling
        // bool falling = velocity.y < 0f || !Input.GetButton("Jump");
        bool falling = velocity.y < 0f || !InputManager.GetInstance().GetJumpPressed();
        float multiplier = falling ? 2f : 1f;

        // Apply gravity and terminal velocity
        velocity.y += gravity * multiplier * Time.deltaTime;
        velocity.y = Mathf.Max(velocity.y, gravity / 2f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            // Bounce off enemy head
            if (transform.DotTest(collision.transform, Vector2.down))
            {
                velocity.y = jumpForce / 2f;
                jumping = true;
            }
        }
        else if (collision.gameObject.layer != LayerMask.NameToLayer("PowerUp"))
        {
            // Stop vertical movement if mario bonks his head
            if (transform.DotTest(collision.transform, Vector2.up)) {
                velocity.y = 0f;
            }
        }

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


}


