using System.Collections.Generic;
using System.Linq;

public class WindowsManager: IService
{
    private Dictionary<BaseWindow, bool> _windows = new Dictionary<BaseWindow, bool>();

    public void Init()
    {
        
    }

    public void RegisterWindow(BaseWindow window)
    {
        _windows.Add(window, false);
    }

    public void OpenWindow(BaseWindow window)
    {
        if(_windows.ContainsKey(window))
        {
            _windows[window] = true;
            window.gameObject.SetActive(true);
            window.ActionAfterOpen();
            UpdateCursorState();
        }
    }

    public void CloseWindow(BaseWindow window)
    {
        if( _windows.ContainsKey(window))
        {
            _windows[window] = false;
            window.gameObject.SetActive(false);
            window.ActionAfterClose();
            UpdateCursorState();
        }
    }

    public void CloseAllWindows()
    {
        foreach(BaseWindow window in _windows.Keys.ToList())
        {
            _windows[window] = false;
            window.gameObject.SetActive(false);
            window.ActionAfterClose();
            UpdateCursorState();
        }
    }

    public void CloseOtherWindows(BaseWindow window)
    {
        foreach (var _window in _windows.Keys.ToList())
        {
            if(_window != window)
            {
                CloseWindow(_window);
            }
        }
    }

    private void UpdateCursorState()
    {
        if(IsWindowsOpened)
        {
            Mouse.UnlockCursor();
        }
        else
        {
            Mouse.LockCursor();
        }
    }

    public bool IsWindowsOpened
    {
        get
        {
            bool isOpened = false;
            foreach(bool value in _windows.Values)
            {
                if(value)
                {
                    isOpened = true;
                }
            }
            return isOpened;
        }
    }
}
