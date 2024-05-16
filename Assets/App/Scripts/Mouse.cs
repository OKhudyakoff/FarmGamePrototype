using InventorySystem.UI;
using UnityEngine;

public class Mouse : MonoBehaviour,IService
{
    [SerializeField] private MouseSlot _mouseSlot;
    public MouseSlot MouseSlot => _mouseSlot;
    public bool IsCursorLocked { get; private set; }

    private void Start()
    {
        LockCursor();
    }

    public void LockCursor()
    {
        IsCursorLocked = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        ServiceLocator.Current.Get<ToolTip>().Hide();
    }

    public void UnlockCursor()
    {
        IsCursorLocked = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void Update()
    {
        transform.position = ServiceLocator.Current.Get<InputHandler>().MousePosition;
    }
}
