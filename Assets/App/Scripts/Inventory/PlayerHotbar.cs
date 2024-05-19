using InventorySystem;
using InventorySystem.Model;
using InventorySystem.UI;
using UnityEngine;

public class PlayerHotbar : InventoryController
{
    [SerializeField] private HudDisplay _hudDisplay;
    [SerializeField] private PlayerInteractor _playerInteractor;
    [SerializeField] private Transform _playerHand;
    private GameObject itemInHand;

    private int _currentSelectedSlotId;
    private InventorySlotDisplay _currentSelectedSlotDisplay { get { return _hudDisplay.Slots[_currentSelectedSlotId]; } }

    public InventorySlot CurrentSlot => _currentSelectedSlotDisplay.InvSlot;

    public override void Init()
    {
        _inventorySize = _hudDisplay.Slots.Count;
        _inventory = new InventoryModel(_inventorySize);
        _hudDisplay.Init(this, _inventory);
    }

    private void Awake()
    {   
        Init();
    }

    private void Start()
    {
        ServiceLocator.Current.Get<InputHandler>().OnScrollTriggered += ChangeCurrentSelectedSlot;
        ServiceLocator.Current.Get<InputHandler>().OnItemInteractTriggered += UseSelectedItem;
        _currentSelectedSlotId = 0;
        ChangeCurrentSelectedSlot(0);
    }

    private void OnDestroy()
    {
        ServiceLocator.Current.Get<InputHandler>().OnScrollTriggered -= ChangeCurrentSelectedSlot;
        CurrentSlot.OnSlotUpdated -= UpdateHandItem;
    }

    public override int AddItem(ItemData itemData, int count)
    {
        return _inventory.AddItem(itemData, count);
    }

    private void ChangeCurrentSelectedSlot(float value)
    {
        if (_hudDisplay.Slots.Count > 0)
        {
            _currentSelectedSlotDisplay.RemoveSelection();
            CurrentSlot.OnSlotUpdated -= UpdateHandItem;
            if (value > 0)
            {
                _currentSelectedSlotId -= 1;
            }
            else if (value < 0)
            {
                _currentSelectedSlotId += 1;
            }
            if (_currentSelectedSlotId >= _hudDisplay.Slots.Count) _currentSelectedSlotId = 0;
            else if (_currentSelectedSlotId < 0) _currentSelectedSlotId = _hudDisplay.Slots.Count - 1;

            _currentSelectedSlotDisplay.SetSelection();
            CurrentSlot.OnSlotUpdated += UpdateHandItem;
            UpdateHandItem();
        }
    }

    private void UpdateHandItem()
    {
        if (itemInHand != null)
        {
            Destroy(itemInHand);
        }

        if (CurrentSlot != null && !CurrentSlot.IsEmpty && CurrentSlot.ItemData.ItemPrefab != null)
        {
            itemInHand = Instantiate(CurrentSlot.ItemData.ItemPrefab, _playerHand);
        }
    }

    private void UseSelectedItem()
    {
        if(CurrentSlot !=  null && !CurrentSlot.IsEmpty)
        {
            IUseable useable = CurrentSlot.ItemData as IUseable;
            if(useable != null)
            {
                useable.Use(_playerInteractor, CurrentSlot);
            }
        }
    }
}
