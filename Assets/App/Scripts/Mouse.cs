using InventorySystem.UI;
using UnityEngine;

public class Mouse : MonoBehaviour,IService
{
    [SerializeField] private MouseSlot _mouseSlot;
    public MouseSlot MouseSlot => _mouseSlot;
    public bool IsCursorLocked { get; private set; }
    private static Mouse _instance;

    private void Awake()
    {
        if(_instance == null)
            _instance = this;
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        LockCursor();
    }

    public static void LockCursor()
    {
        _instance.IsCursorLocked = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        ServiceLocator.Current.Get<ToolTip>().Hide();
        _instance._mouseSlot.ClearSlot();
    }

    public static void UnlockCursor()
    {
        _instance.IsCursorLocked = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void Update()
    {
        transform.position = ServiceLocator.Current.Get<InputHandler>().MousePosition;
    }
}
