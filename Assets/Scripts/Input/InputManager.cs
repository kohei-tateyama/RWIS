using UnityEngine;
using UnityEngine.InputSystem;

// This script acts as a single point for all other scripts to get
// the current input from. It uses Unity's new Input System and
// functions should be mapped to their corresponding controls
// using a PlayerInput component with Unity Events.

[RequireComponent(typeof(PlayerInput))]
public class InputManager : MonoBehaviour
{
    private Vector2 moveDirection = Vector2.zero;
    private bool jumpPressed = false;
    private bool interactPressed = false;
    private bool pausePressed = false;
    private bool submitPressed = false;

    public static InputManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Found more than one Input Manager in the scene.");
        }
        Instance = this;
    }

    public void MovePressed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            moveDirection = context.ReadValue<Vector2>();
        }
        else if (context.canceled)
        {
            moveDirection = context.ReadValue<Vector2>();
        } 
    }

    public void JumpPressed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            jumpPressed = true;
        }
        else if (context.canceled)
        {
            jumpPressed = false;
        }
    }

    public void InteractButtonPressed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            interactPressed = true;
        }
        else if (context.canceled)
        {
            interactPressed = false;
        } 
    }


    public void PauseButtonPressed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            pausePressed = true;
        }
        else if (context.canceled)
        {
            pausePressed = false;
        } 
    }

    public void SubmitPressed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            submitPressed = true;
        }
        else if (context.canceled)
        {
            submitPressed = false;
        } 
    }

    public Vector2 GetMoveDirection() 
    {
        return moveDirection;
    }

    // for any of the below 'Get' methods, if we're getting it then we're also using it,
    // which means we should set it to false so that it can't be used again until actually
    // pressed again.

    public bool GetJumpPressed() 
    {
        bool result = jumpPressed;
        //jumpPressed = false;
        return result;
    }

    public bool GetInteractPressed() 
    {
        bool result = interactPressed;
        interactPressed = false;
        return result;
    }

    public bool GetInteractHeld()
    {
        // Returns the current state without resetting it
        return interactPressed;
    }
    
    public bool GetPausePressed() 
    {
        bool result = pausePressed;
        pausePressed = false;
        return result;
    }

    public bool GetSubmitPressed() 
    {
        bool result = submitPressed;
        submitPressed = false;
        return result;
    }

    public void RegisterSubmitPressed() 
    {
        submitPressed = false;
    }

}