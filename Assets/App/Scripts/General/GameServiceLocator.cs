using InventorySystem;
using UnityEngine;
using Utilities;

public class GameServiceLocator : MonoBehaviour
{
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private InputHandler _inputHandler;
    [SerializeField] private TimeManager _timeManager;
    [SerializeField] private Mouse _mouse;
    [SerializeField] private PlayerInventory _inventoryController;
    //[SerializeField] private BulletObjectPool _bulletObjectPool;

    private void Awake()
    {
        ServiceLocator.Initialize();
        RegisterServices();
        InitServices();
    }

    private void RegisterServices()
    {
        ServiceLocator.Current.Register<PlayerController>(_playerController);
        ServiceLocator.Current.Register<InputHandler>(_inputHandler);
        ServiceLocator.Current.Register<TimeManager>(_timeManager);
        ServiceLocator.Current.Register<Mouse>(_mouse);
        ServiceLocator.Current.Register<PlayerInventory>(_inventoryController);

        //ServiceLocator.Current.Register<BulletObjectPool>(_bulletObjectPool);
    }

    private void InitServices()
    {
        _inventoryController.Init();
        _timeManager.Init();
    }
}
