using InventorySystem;
using UnityEngine;
using Utilities;

public class Menu : MonoBehaviour, IService
{
    [SerializeField] private BaseWindow _menuPanel;
    [SerializeField] private TabSystem _tabSystem;
    private InputHandler _inputHandler;
    private PlayerInventory _inventoryController;
    private TimeManager _timeManager;
    private bool _isMenuOpened = false;


    private void Start()
    {
        _inputHandler = ServiceLocator.Current.Get<InputHandler>();
        _inventoryController = ServiceLocator.Current.Get<PlayerInventory>();
        _timeManager = ServiceLocator.Current.Get<TimeManager>();

        _inputHandler.OnInventoryTriggered += InventoryTriggered;
        _inputHandler.OnPauseTriggered += PauseTriggered;

        _tabSystem.Init();
        _isMenuOpened = false;
    }

    private void OnDisable()
    {
        _inputHandler.OnInventoryTriggered -= InventoryTriggered;
        _inputHandler.OnPauseTriggered -= PauseTriggered;
    }

    private void OpenTab(int tabID)
    {
        if(_isMenuOpened && tabID == _tabSystem.CurrentTabID || _isMenuOpened && tabID == 2)
        {
            _timeManager.ContinueTime();
            _isMenuOpened = false;
            ServiceLocator.Current.Get<WindowsManager>().CloseAllWindows();
            _inventoryController.SetOtherInventory(null);
        }
        else
        {
            _timeManager.PauseTime();
            _tabSystem.SwitchTab(tabID);
            _isMenuOpened = true;
            _menuPanel.Open();
            _inventoryController.ConnectWithHootbar();
        }
    }

    public void PauseTriggered()
    {
        OpenTab(2);
    }

    public void InventoryTriggered()
    {
        OpenTab(0);
    }

    public void CalendarTriggered()
    {
        OpenTab(1);
    }
}
