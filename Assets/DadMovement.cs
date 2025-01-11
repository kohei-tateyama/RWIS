using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class DadMovement : MonoBehaviour
{
    // private Camera mainCamera;
    private Rigidbody2D rb;
    private Collider2D capsuleCollider;

    private Vector2 velocity;
    private Vector2 inputAxis;
    public bool grounded { get; private set; }
    public float maxJumpHeight = 5f;
    public float maxJumpTime = 1f;
    public float gravity => (-2f * maxJumpHeight) / Mathf.Pow(maxJumpTime / 2f, 2f);
    public float moveSpeed = 1f;
    public bool running => Mathf.Abs(velocity.x) > 0.25f || Mathf.Abs(inputAxis.x) > 0.25f;
    private bool isFollowing = false;


    private void Awake()
    {
        // mainCamera = Camera.main;
        rb = GetComponent<Rigidbody2D>();
        capsuleCollider = GetComponent<Collider2D>();
    }

    private void Start(){
        // Subscribe to the event to get updates when SocialMeterValue changes
        DialogueManager.Instance.OnFollowingMCEvent += Follow;
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
        if (DialogueManager.Instance.dialogueIsPlaying || PauseMenuManager.GetInstance().isPauseMenuOn )//|| !isFollowing)
        {
            return;
        }
        // if dad is far enough from MC
        HorizontalMovement();

        ApplyGravity();
    }
    private void FixedUpdate()
    {
        // Move mario based on his velocity
        Vector2 position = rb.position;
        position += velocity * Time.fixedDeltaTime;

        // Clamp within the screen bounds
        // Vector2 leftEdge = mainCamera.ScreenToWorldPoint(Vector2.zero);
        // Vector2 rightEdge = mainCamera.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        // position.x = Mathf.Clamp(position.x, leftEdge.x + 0.5f, rightEdge.x - 0.5f);

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
        if (velocity.x > 0f) {
            transform.eulerAngles = Vector3.zero;
        } else if (velocity.x < 0f) {
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
        }
    }


    private void ApplyGravity()
    {
        // Check if falling
        // bool falling = velocity.y < 0f || !Input.GetButton("Jump");
        bool falling = velocity.y < 0f || !InputManager.Instance.GetJumpPressed();
        float multiplier = falling ? 2f : 1f;

        // Apply gravity and terminal velocity
        velocity.y += gravity * multiplier * Time.deltaTime;
        velocity.y = Mathf.Max(velocity.y, gravity / 2f);
    }

    private void Follow(int followingMC){
        Debug.Log("inside follow function" + followingMC);
        if (followingMC==1){
            isFollowing = true;
            BoxCollider2D box = new BoxCollider2D();
            box = gameObject.GetComponentInChildren<BoxCollider2D>();
            box.enabled = false;
        } else {
            isFollowing = false;
        }
    }
    void OnDestroy()
    {
        // Unsubscribe from the event to prevent memory leaks
        DialogueManager.Instance.OnFollowingMCEvent -= Follow;
    }


}








