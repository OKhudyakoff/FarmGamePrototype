using System;
using System.Collections.Generic;
using InventorySystem.Controllers;
using InventorySystem.Model;
using InventorySystem.UI;
using UnityEngine;

public class PlayerHotbar : InventoryController
{
    [SerializeField] private HudDisplay _hudDisplay;
    [SerializeField] private PlayerInteractor _playerInteractor;
    [SerializeField] private Transform _playerHand;
    private GameObject itemInHand;
    private Dictionary<ItemData, GameObject> itemPool = new Dictionary<ItemData, GameObject>();

    private int _currentSelectedSlotId;
    private InventorySlotDisplay CurrentSelectedSlotDisplay => _hudDisplay.Slots[_currentSelectedSlotId];
    public InventorySlot CurrentSlot => CurrentSelectedSlotDisplay.InvSlot;

    public override void Init()
    {
        _inventorySize = _hudDisplay.Slots.Count;
        base.Init();
        _hudDisplay.Init(this, _inventory);
    }

    private void Start()
    {
        Init();
        RegisterInputEvents();
        ConnectWithOtherInventory(ServiceLocator.Current.Get<PlayerInventory>());
        _currentSelectedSlotId = 0;
        ChangeCurrentSelectedSlot(0);
    }

    private void OnDestroy()
    {
        UnregisterInputEvents();
        CurrentSlot.OnSlotUpdated -= UpdateHandItem;
    }

    public override int AddItem(ItemData itemData, int count)
    {
        return _inventory.AddItem(itemData, count);
    }

    private void RegisterInputEvents()
    {
        var inputHandler = ServiceLocator.Current.Get<InputHandler>();
        inputHandler.OnScrollTriggered += ChangeCurrentSelectedSlot;
        inputHandler.OnRightBtnUseTriggered += RightBtnUseSelectedItem;
        inputHandler.OnLeftBtnUseTriggered += LeftBtnUseSelectedItem;
    }

    private void UnregisterInputEvents()
    {
        var inputHandler = ServiceLocator.Current.Get<InputHandler>();
        inputHandler.OnScrollTriggered -= ChangeCurrentSelectedSlot;
        inputHandler.OnRightBtnUseTriggered -= RightBtnUseSelectedItem;
        inputHandler.OnLeftBtnUseTriggered -= LeftBtnUseSelectedItem;
    }

    private void ChangeCurrentSelectedSlot(float value)
    {
        if (!ServiceLocator.Current.Get<Mouse>().IsCursorLocked) return;

        if (_hudDisplay.Slots.Count > 0)
        {
            CurrentSelectedSlotDisplay.RemoveSelection();
            CurrentSlot.OnSlotUpdated -= UpdateHandItem;
            _currentSelectedSlotId = (int)Mathf.Repeat(_currentSelectedSlotId - value, _hudDisplay.Slots.Count);
            CurrentSelectedSlotDisplay.SetSelection();
            CurrentSlot.OnSlotUpdated += UpdateHandItem;
            UpdateHandItem();
        }
    }

    private void UpdateHandItem()
    {
        if (itemInHand != null)
        {
            itemInHand.SetActive(false);
        }

        if (CurrentSlot != null && !CurrentSlot.IsEmpty && CurrentSlot.ItemData.ItemPrefab != null)
        {
            if (!itemPool.TryGetValue(CurrentSlot.ItemData, out itemInHand))
            {
                itemInHand = Instantiate(CurrentSlot.ItemData.ItemPrefab, _playerHand);
                itemPool[CurrentSlot.ItemData] = itemInHand;
            }
            itemInHand.SetActive(true);
        }
    }

    private void UseSelectedItem(Action<IUsable, PlayerInteractor, InventorySlot, System.Action> useAction)
    {
        if (CurrentSlot != null && !CurrentSlot.IsEmpty)
        {
            if (CurrentSlot.ItemData is IUsable useable)
            {
                useAction(useable, _playerInteractor, CurrentSlot, () => {
                    GameDebugger.ShowInfo("Действие выполнено");
                });
            }
        }
    }

    private void RightBtnUseSelectedItem()
    {
        UseSelectedItem((useable, interactor, slot, onComplete) => useable.RightBtnUse(interactor, slot, onComplete));
    }

    private void LeftBtnUseSelectedItem()
    {
        UseSelectedItem((useable, interactor, slot, onComplete) => useable.LeftBtnUse(interactor, slot, onComplete));
    }
}
