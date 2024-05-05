using InventorySystem;
using UnityEngine;
using Utilities;

public class UIController : MonoBehaviour, IService
{
    [SerializeField] private GameObject _pausePanel;
    private InputHandler _inputHandler;
    private PlayerInventory _inventoryController;
    private Mouse _mouse;
    private TimeManager _timeManager;


    private void Start()
    {
        _inputHandler = ServiceLocator.Current.Get<InputHandler>();
        _inventoryController = ServiceLocator.Current.Get<PlayerInventory>();
        _mouse = ServiceLocator.Current.Get<Mouse>();
        _timeManager = ServiceLocator.Current.Get<TimeManager>();

        _inputHandler.OnInventoryTriggered += _inventoryController.TriggerInventory;
        _inputHandler.OnPauseTriggered += PauseTriggered;
    }

    public void ShowPauseView()
    {
        _mouse.UnlockCursor();
        _timeManager.PauseTime();
        _pausePanel.SetActive(true);
    }

    public void HidePauseView()
    {
        _mouse.LockCursor();
        _timeManager.ContinueTime();
        _pausePanel.SetActive(false);
    }

    public void PauseTriggered()
    {
        if (_pausePanel.activeSelf)
        {
            HidePauseView();
        }
        else
        {
            ShowPauseView();
        }
    }
}
