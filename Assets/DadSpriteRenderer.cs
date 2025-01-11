using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(SpriteRenderer))]
public class DadSpriteRenderer : MonoBehaviour
{
    private DadMovement movement;
    public SpriteRenderer spriteRenderer { get; private set; }
    public Sprite idle;
    // public Sprite jump;
    // public Sprite slide;
    public AnimatedSprite run;

    private void Awake()
    {
        movement = GetComponentInParent<DadMovement>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void LateUpdate()
    {
        run.enabled = movement.running;

        // if (movement.jumping) {
        //     spriteRenderer.sprite = jump;
        // } else if (movement.sliding) {
        //     spriteRenderer.sprite = slide;
        // } else if (!movement.running) {
            if (!movement.running) {
            spriteRenderer.sprite = idle;
        }
    }

    private void OnEnable()
    {
        spriteRenderer.enabled = true;
    }

    private void OnDisable()
    {
        spriteRenderer.enabled = false;
        run.enabled = false;
    }

}
