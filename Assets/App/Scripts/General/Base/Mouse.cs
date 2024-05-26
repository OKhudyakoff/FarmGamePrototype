using UnityEngine;

public class Mouse : MonoBehaviour,IService
{
    public bool IsCursorLocked { get; private set; }
    private static Mouse _instance;

    public void Init()
    {
        if(_instance == null)
            _instance = this;
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
        ServiceLocator.Current.Get<ToolTipController>().Hide();
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
