using InventorySystem.Model;
using InventorySystem.UI;
using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem
{
    public abstract class InventoryController : MonoBehaviour, ISlotHolder
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
            if (_inventory.ItemContainedCount(itemData) >= count)
            {
                _inventory.RemoveItem(itemData, count);
            }
        }

        public virtual void OnLeftBtnSlotClicked(InventorySlotDisplay slot)
        {
            MouseSlot _mouseSlot = ServiceLocator.Current.Get<Mouse>().MouseSlot;
            if (playerHaveAccess)
            {
                if (!slot.InvSlot.IsEmpty)
                {
                    if (_mouseSlot.IsEmpty)
                    {
                        _mouseSlot.Set(slot.InvSlot);
                    }
                    else
                    {
                        if (_mouseSlot.Slot == slot.InvSlot)
                        {
                            _mouseSlot.ClearSlot();
                        }
                        else if (_mouseSlot.Slot.ItemData == slot.InvSlot.ItemData)
                        {
                            if (_mouseSlot.Slot.StackSize > slot.InvSlot.ItemData.MaxStackSize - slot.InvSlot.StackSize)
                            {
                                int countToAdd = slot.InvSlot.ItemData.MaxStackSize - slot.InvSlot.StackSize;
                                slot.InvSlot.IncreaseQuantity(countToAdd);
                                _mouseSlot.Slot.DecreaseQuantity(countToAdd);
                            }
                            else
                            {
                                slot.InvSlot.IncreaseQuantity(_mouseSlot.Slot.StackSize);
                                _mouseSlot.Slot.DecreaseQuantity(_mouseSlot.Slot.StackSize);
                            }
                        }
                        else
                        {
                            // Swap items
                            SwapSlots(_mouseSlot.Slot, slot.InvSlot);
                        }
                    }
                }
                else
                {
                    if (!_mouseSlot.IsEmpty)
                    {
                        slot.InvSlot.SetItem(_mouseSlot.Slot.ItemData, _mouseSlot.Slot.StackSize);
                        _mouseSlot.Slot.DecreaseQuantity(_mouseSlot.Slot.StackSize);
                    }
                }
            }
        }

        public void SwapSlots(InventorySlot slotA, InventorySlot slotB)
        {
            InventorySlot tmpSlot = new InventorySlot();
            tmpSlot.SetItem(slotB.ItemData, slotB.StackSize);
            slotB.SetItem(slotA.ItemData, slotA.StackSize);
            slotA.SetItem(tmpSlot.ItemData, tmpSlot.StackSize, slotA.IsLockedToDisplay);
        }

        public virtual void OnRightBtnSlotClicked(InventorySlotDisplay slot)
        {
            if(playerHaveAccess)
            {
                MouseSlot _mouseSlot = ServiceLocator.Current.Get<Mouse>().MouseSlot;
                if (!slot.InvSlot.IsEmpty && _mouseSlot.IsEmpty)
                {
                    _mouseSlot.SplitStack(slot.InvSlot);
                }
            }
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
            return _inventory.ItemContainedCount(item);
        }
    }
}
