using UnityEngine;

public class BaseWindow : MonoBehaviour
{
    protected WindowsManager _windowsManager;

    public virtual void Init()
    {
        _windowsManager = ServiceLocator.Current.Get<WindowsManager>();
        _windowsManager.RegisterWindow(this);
    }

    public void Open()
    {
        if(_windowsManager == null)
        {
            Init();
        }
        _windowsManager.OpenWindow(this);
    }

    public virtual void ActionAfterOpen()
    {
        
    }

    public void Close()
    {
        if (_windowsManager == null)
        {
            Init();
        }
        _windowsManager.CloseWindow(this);
    }

    public virtual void ActionAfterClose()
    {
        
    }
}
