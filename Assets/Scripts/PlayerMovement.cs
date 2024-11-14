using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private new Rigidbody2D rigidbody;

    public float moveSpeed = 8f;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }
}
