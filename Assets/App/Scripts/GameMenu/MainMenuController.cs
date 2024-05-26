using InventorySystem.Controllers;
using UnityEngine;
using Utilities;

public class MainMenuController: MonoBehaviour, IService
{
    [SerializeField] private BaseWindow _menuPanel;
    [SerializeField] private TabSystem _tabSystem;
    [SerializeField] private GameSettingsUI _gameSettingsUI;
    private InputHandler _inputHandler;
    private PlayerInventory _inventoryController;
    private TimeManager _timeManager;
    private bool _isMenuOpened = false;


    public void Init()
    {
        _inputHandler = ServiceLocator.Current.Get<InputHandler>();
        _inventoryController = ServiceLocator.Current.Get<PlayerInventory>();
        _timeManager = ServiceLocator.Current.Get<TimeManager>();

        _inputHandler.OnInventoryTriggered += InventoryTriggered;
        _inputHandler.OnPauseTriggered += PauseTriggered;

        _tabSystem.Init();
        _gameSettingsUI.Init();
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
            _inventoryController.DisconnectWithOtherInventory();
        }
        else
        {
            _timeManager.PauseTime();
            _tabSystem.SwitchTab(tabID);
            _isMenuOpened = true;
            _menuPanel.Open();
            _inventoryController.ConnectWithOtherInventory();
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
