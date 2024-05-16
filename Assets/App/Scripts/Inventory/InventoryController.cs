using InventorySystem.Model;
using InventorySystem.UI;
using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem
{
    public abstract class InventoryController : MonoBehaviour
    {
        [SerializeField] private bool playerHaveAccess;
        protected int _inventorySize;
        protected InventoryModel _inventory;
        public virtual void Init()
        {
            _inventory = new InventoryModel(_inventorySize);
        }

        public abstract int AddItem(ItemData itemData, int count);

        public void RemoveItem(ItemData itemData, int count)
        {
            if (_inventory.GetItemCount(itemData) >= count)
            {
                _inventory.RemoveItem(itemData, count);
            }
        }

        public void OnSlotLeftClicked(InventorySlotDisplay clickedSlotDisplay)
        {
            MouseSlot _mouseSlot = ServiceLocator.Current.Get<Mouse>().MouseSlot;
            if (playerHaveAccess)
            {
                if (!clickedSlotDisplay.InvSlot.IsEmpty)
                {
                    if (_mouseSlot.IsEmpty)
                    {
                        clickedSlotDisplay.HideUI();
                        _mouseSlot.Set(clickedSlotDisplay.InvSlot);
                    }
                    else
                    {
                        if (_mouseSlot.Slot == clickedSlotDisplay.InvSlot)
                        {
                            _mouseSlot.ClearSlot();
                        }
                        else if (_mouseSlot.Slot.ItemData == clickedSlotDisplay.InvSlot.ItemData)
                        {
                            if (_mouseSlot.Slot.StackSize > clickedSlotDisplay.InvSlot.ItemData.MaxStackSize - clickedSlotDisplay.InvSlot.StackSize)
                            {
                                int countToAdd = clickedSlotDisplay.InvSlot.ItemData.MaxStackSize - clickedSlotDisplay.InvSlot.StackSize;
                                clickedSlotDisplay.InvSlot.AddItem(countToAdd);
                                _mouseSlot.Slot.RemoveItem(countToAdd);
                            }
                            else
                            {
                                clickedSlotDisplay.InvSlot.AddItem(_mouseSlot.Slot.StackSize);
                                _mouseSlot.Slot.RemoveItem(_mouseSlot.Slot.StackSize);
                            }
                        }
                        else
                        {
                            // Swap items
                            SwapSlots(_mouseSlot.Slot, clickedSlotDisplay.InvSlot);
                        }
                    }
                }
                else
                {
                    if (!_mouseSlot.IsEmpty)
                    {
                        clickedSlotDisplay.InvSlot.SetItem(_mouseSlot.Slot.ItemData, _mouseSlot.Slot.StackSize);
                        _mouseSlot.Slot.RemoveItem(_mouseSlot.Slot.StackSize);
                    }
                }
                UpdateDisplay();
            }
        }

        public virtual void UpdateDisplay() {}

        public void SwapSlots(InventorySlot slotA, InventorySlot slotB)
        {
            InventorySlot tmpSlot = new InventorySlot();
            tmpSlot.SetItem(slotB.ItemData, slotB.StackSize);
            slotB.SetItem(slotA.ItemData, slotA.StackSize);
            slotA.SetItem(tmpSlot.ItemData, tmpSlot.StackSize);
        }

        public void OnSlotRightClicked(InventorySlotDisplay clickedSlotDisplay)
        {
            if(playerHaveAccess)
            {
                MouseSlot _mouseSlot = ServiceLocator.Current.Get<Mouse>().MouseSlot;
                if (!clickedSlotDisplay.InvSlot.IsEmpty && _mouseSlot.IsEmpty)
                {
                    _mouseSlot.SplitStack(clickedSlotDisplay.InvSlot);
                }
            }
        }

        public void OnPointerEnter(InventorySlot slot)
        {
            if(!slot.IsEmpty)
                ServiceLocator.Current.Get<ToolTip>().Show(slot.ItemData.ItemDescription, slot.ItemData.ItemName, slot.ItemData.ItemSprite);
        }

        public void OnPointerExit()
        {
            ServiceLocator.Current.Get<ToolTip>().Hide();
        }

        public List<ItemData> ItemsInInventory()
        {
            List<ItemData> listToReturn = new List<ItemData>();
            for (int i = 0; i < _inventory.Slots.Count; i++)
            {
                if(!_inventory.Slots[i].IsEmpty && !listToReturn.Contains(_inventory.Slots[i].ItemData))
                {
                    listToReturn.Add(_inventory.Slots[i].ItemData);
                }
            }
            return listToReturn;
        }

        public int ItemCount(ItemData item)
        {
            return _inventory.GetItemCount(item);
        }
    }
}
