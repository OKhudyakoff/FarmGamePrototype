using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour, PlayerControls.IMovementActions, PlayerControls.IUIActions, IService
{
    public Vector2 MouseDelta { get; private set; }
    public Vector2 MoveComposite {  get; private set; }
    public Vector2 MousePosition { get; private set; }
    public bool IsRunning { get; private set; }

    public Action OnJumpPerformed;
    public Action OnInteractionTriggered;
    public Action OnItemInteractTriggered;

    public bool IsSplitting { get; private set; }

    public Action OnInventoryTriggered;
    public Action OnPauseTriggered;
    public Action<float> OnScrollTriggered;

    private PlayerControls controls;

    private void OnEnable()
    {
        if (controls != null)
            return;

        controls = new PlayerControls();

        controls.Movement.SetCallbacks(this);
        controls.Movement.Enable();

        controls.UI.SetCallbacks(this);
        controls.UI.Enable();
    }

    public void OnDisable()
    {
        controls.Movement.Disable();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            OnJumpPerformed?.Invoke();
        }

    }

    public void OnMouseLook(InputAction.CallbackContext context)
    {
        MouseDelta = context.ReadValue<Vector2>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        MoveComposite = context.ReadValue<Vector2>();
    }

    public void OnRun(InputAction.CallbackContext context)
    {
        if(context.performed) IsRunning = true;
        else if (context.canceled) IsRunning = false;
    }

    public void OnMousePosition(InputAction.CallbackContext context)
    {
        MousePosition = context.ReadValue<Vector2>();
    }

    public void OnTriggerInventory(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            OnInventoryTriggered?.Invoke();
        }
    }

    public void OnInteraction(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            OnInteractionTriggered?.Invoke();
        }
    }

    public void OnScroll(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            OnScrollTriggered?.Invoke(context.ReadValue<float>());
        }
    }

    public void OnPause(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            OnPauseTriggered?.Invoke();
        }
    }

    public void OnItemInteraction(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            OnItemInteractTriggered?.Invoke();
        }
    }

    public void OnSplit(InputAction.CallbackContext context)
    {
        if (context.performed) IsSplitting = true;
        else if (context.canceled) IsSplitting = false;
    }
}
