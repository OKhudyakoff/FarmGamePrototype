using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour, PlayerControls.IPlayerControlActions, PlayerControls.IUIControlActions, IService
{
    public Vector2 MouseDelta { get; private set; }
    public Vector2 MoveComposite {  get; private set; }
    public Vector2 MousePosition { get; private set; }
    public bool IsRunning { get; private set; }

    public Action OnJumpPerformed;
    public Action OnInteractionTriggered;
    public Action OnRightBtnUseTriggered;
    public Action OnLeftBtnUseTriggered;


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

        controls.PlayerControl.SetCallbacks(this);
        controls.PlayerControl.Enable();

        controls.UIControl.SetCallbacks(this);
        controls.UIControl.Enable();
    }

    public void OnDisable()
    {
        controls.PlayerControl.Disable();
        controls.UIControl.Disable();
    }

    public void Init()
    {
        
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

    public void OnRightBtnUse(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            OnRightBtnUseTriggered?.Invoke();
        }
    }

    public void OnLeftBtnUse(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            OnLeftBtnUseTriggered?.Invoke();
        }
    }

    public void OnQuicklyShift(InputAction.CallbackContext context)
    {
        if (context.performed) IsSplitting = true;
        else if (context.canceled) IsSplitting = false;
    }
}
