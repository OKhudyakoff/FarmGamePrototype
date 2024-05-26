using InventorySystem.Controllers;
using InventorySystem.UI;
using UnityEngine;
using Utilities;

public class GameServiceLocator : MonoBehaviour
{
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private InputHandler _inputHandler;
    [SerializeField] private TimeManager _timeManager;
    [SerializeField] private Mouse _mouse;
    [SerializeField] private MouseItemSlot _mouseItemSlot;
    [SerializeField] private PlayerInventory _inventoryController;
    [SerializeField] private ToolTipController _toolTip;
    [SerializeField] private MainMenuController _mainMenuController;
    [SerializeField] private GameDebugger _gameDebugger;

    private ItemDatabase _itemDatabase;
    private RecipeDatabase _recipeDatabase;
    private WindowsManager _windowsManager;

    private void Awake()
    {
        ServiceLocator.Initialize();

        _windowsManager = new WindowsManager();
        _itemDatabase = new ItemDatabase();
        _recipeDatabase = new RecipeDatabase();

        RegisterServices();
        InitServices();
    }

    private void RegisterServices()
    {
        ServiceLocator.Current.Register<InputHandler>(_inputHandler); // General
        ServiceLocator.Current.Register<GameDebugger>(_gameDebugger);
        ServiceLocator.Current.Register<TimeManager>(_timeManager);
        ServiceLocator.Current.Register<Mouse>(_mouse);
        ServiceLocator.Current.Register<MouseItemSlot>(_mouseItemSlot);
        ServiceLocator.Current.Register<ToolTipController>(_toolTip);
        ServiceLocator.Current.Register<WindowsManager>(_windowsManager);
        ServiceLocator.Current.Register<PlayerController>(_playerController);
        ServiceLocator.Current.Register<PlayerInventory>(_inventoryController);
        ServiceLocator.Current.Register<MainMenuController>(_mainMenuController);
        ServiceLocator.Current.Register<ItemDatabase>(_itemDatabase);
        ServiceLocator.Current.Register<RecipeDatabase>(_recipeDatabase);
    }

    private void InitServices()
    {
        ServiceLocator.Current.Get<InputHandler>().Init();
        ServiceLocator.Current.Get<GameDebugger>().Init();
        ServiceLocator.Current.Get<TimeManager>().Init();
        ServiceLocator.Current.Get<Mouse>().Init();
        ServiceLocator.Current.Get<MouseItemSlot>().Init();
        ServiceLocator.Current.Get<ToolTipController>().Init();
        ServiceLocator.Current.Get<PlayerController>().Init();
        ServiceLocator.Current.Get<PlayerInventory>().Init();
        ServiceLocator.Current.Get<WindowsManager>().Init();
        ServiceLocator.Current.Get<ItemDatabase>().Init();
        ServiceLocator.Current.Get<RecipeDatabase>().Init();
        ServiceLocator.Current.Get<MainMenuController>().Init();
    }
}
