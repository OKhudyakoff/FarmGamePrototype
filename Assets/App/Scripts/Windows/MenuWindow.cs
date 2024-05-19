public class MenuWindow: BaseWindow
{
    public override void ActionAfterOpen()
    {
        base.ActionAfterOpen();
        _windowsManager.CloseOtherWindows(this);
    }
}
