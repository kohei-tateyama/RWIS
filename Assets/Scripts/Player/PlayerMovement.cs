using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    private Camera mainCamera;
    private Rigidbody2D rb;
    private Collider2D capsuleCollider;

    private Vector2 velocity;
    private Vector2 inputAxis;

    public float moveSpeed = 12f;
    public bool running => Mathf.Abs(velocity.x) > 0.25f || Mathf.Abs(inputAxis.x) > 0.25f;
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
        inputAxis = InputManager.Instance.GetMoveDirection();
        
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


