using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private new Rigidbody2D rigidbody;
    // private new Camera camera;
    private Vector2 velocity;
    private float inputAxis;

    public float moveSpeed = 8f;
    public int maxmeter = 100;
    public int currentmeter;
    public SocialMeter socialMeter;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        currentmeter = maxmeter;
        socialMeter.SetMaxMeter(maxmeter);
    }

    //This function gets called every frame the game is running.
    //We deal with inputs in this function
    private void Update()
    {
        HorizontalMovement();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(20);
        }
        
    }

    void TakeDamage(int damage)
    {
        currentmeter -= damage;
        socialMeter.SetMeter(currentmeter);
    }

    private void HorizontalMovement()
    {
        inputAxis = Input.GetAxis("Horizontal");
        velocity.x = Mathf.MoveTowards(velocity.x, inputAxis * moveSpeed, moveSpeed * Time.deltaTime);
    }

    //Gets called every fixed frames/seconds
    private void FixedUpdate()
    {
        Vector2 position = rigidbody.position;
        position += velocity * Time.fixedDeltaTime;

        rigidbody.MovePosition(position);
    }
}
