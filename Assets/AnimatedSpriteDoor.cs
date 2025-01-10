using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedSpriteDoor : MonoBehaviour
{
    [SerializeField] private Door door;
    [SerializeField] private GameObject touchInputPanel;
    public Transform trackedObject;
    public float homeHeight;
    public float homeThreshold;
    public float classroomHeight;
    public float classroomThreshold;

    public Sprite[] sprites;
    public float framerate = 1f / 6f;

    private SpriteRenderer spriteRenderer;
    public int frame;
    private Coroutine checkCoroutine;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
    }

    private void OnDisable()
    {
    }

    public void EnterDoor()
    {
        frame++;

        if (frame >= 0 && frame < sprites.Length)
        {
            spriteRenderer.sprite = sprites[frame];
        }
        else
        {
            // Stop the repeating invocation once the animation completes
            CancelInvoke("EnterDoor");

        }
    }

    public void ExitDoor()
    {
        // Debug.Log("ExitDoor");
        frame--;

        if (frame >= 0 && frame < sprites.Length)
        {
            spriteRenderer.sprite = sprites[frame];
        }
        else
        {
            // Stop the repeating invocation once the animation completes
            CancelInvoke("ExitDoor");
        }
    }


}
