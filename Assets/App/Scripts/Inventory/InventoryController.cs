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
        protected InputHandler _inputHandler;
        protected Mouse _mouse;
        protected InventoryController _otherInventory;

        protected bool IsSplitting => _inputHandler.IsSplitting;

        public virtual void Init()
        {
            _inventory = new InventoryModel(_inventorySize);
            _inputHandler = ServiceLocator.Current.Get<InputHandler>();
            _mouse = ServiceLocator.Current.Get<Mouse>();
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
            if (!playerHaveAccess) return;

            MouseSlot mouseSlot = _mouse.MouseSlot;
            if (!slot.InvSlot.IsEmpty)
            {
                HandleNonEmptySlotLeftClick(slot, mouseSlot);
            }
            else if (!mouseSlot.IsEmpty)
            {
                slot.InvSlot.SetItem(mouseSlot.Slot.ItemData, mouseSlot.Slot.StackSize);
                mouseSlot.Slot.DecreaseQuantity(mouseSlot.Slot.StackSize);
            }
        }

        private void HandleNonEmptySlotLeftClick(InventorySlotDisplay slot, MouseSlot mouseSlot)
        {
            if (mouseSlot.IsEmpty)
            {
                if (!IsSplitting)
                {
                    mouseSlot.Set(slot.InvSlot);
                }
                else if (_otherInventory != null)
                {
                    int amountRemaining = _otherInventory.AddItem(slot.InvSlot.ItemData, slot.InvSlot.StackSize);
                    slot.InvSlot.SetItem(slot.InvSlot.ItemData, amountRemaining);
                }
            }
            else if (mouseSlot.Slot.ItemData == slot.InvSlot.ItemData)
            {
                MergeSlots(slot.InvSlot, mouseSlot.Slot);
            }
            else
            {
                SwapSlots(mouseSlot.Slot, slot.InvSlot);
            }
        }

        public virtual void OnRightBtnSlotClicked(InventorySlotDisplay slot)
        {
            if (!playerHaveAccess) return;

            MouseSlot mouseSlot = _mouse.MouseSlot;
            if (mouseSlot.IsEmpty && !slot.InvSlot.IsEmpty)
            {
                mouseSlot.SplitStack(slot.InvSlot);
            }
            else if (!mouseSlot.IsEmpty)
            {
                HandleRightClickWithMouseSlot(slot, mouseSlot);
            }
        }

        private void HandleRightClickWithMouseSlot(InventorySlotDisplay slot, MouseSlot mouseSlot)
        {
            if (!slot.InvSlot.IsEmpty && mouseSlot.Slot.ItemData == slot.InvSlot.ItemData)
            {
                mouseSlot.Slot.DecreaseQuantity(1);
                slot.InvSlot.IncreaseQuantity(1);
            }
            else if (slot.InvSlot.IsEmpty)
            {
                slot.InvSlot.SetItem(mouseSlot.Slot.ItemData, 1);
                mouseSlot.Slot.DecreaseQuantity(1);
            }
        }

        private void MergeSlots(InventorySlot slot, InventorySlot mouseSlot)
        {
            int maxStackSize = slot.ItemData.MaxStackSize;
            int countToAdd = Mathf.Min(mouseSlot.StackSize, maxStackSize - slot.StackSize);
            slot.IncreaseQuantity(countToAdd);
            mouseSlot.DecreaseQuantity(countToAdd);
        }

        public void SwapSlots(InventorySlot slotA, InventorySlot slotB)
        {
            InventorySlot tmpSlot = new InventorySlot();
            tmpSlot.SetItem(slotB.ItemData, slotB.StackSize);
            slotB.SetItem(slotA.ItemData, slotA.StackSize);
            slotA.SetItem(tmpSlot.ItemData, tmpSlot.StackSize);
        }

        public List<ItemData> ItemsInInventory()
        {
            List<ItemData> items = new List<ItemData>();
            foreach (var slot in _inventory.Slots)
            {
                if (!slot.IsEmpty && !items.Contains(slot.ItemData))
                {
                    items.Add(slot.ItemData);
                }
            }
            return items;
        }

        public int ItemCount(ItemData item)
        {
            return _inventory.ItemContainedCount(item);
        }

        public void SetOtherInventory(InventoryController otherController)
        {
            _otherInventory = otherController;
        }

        public InventoryModel GetInventory()
        {
            return _inventory;
        }

        public virtual void UpdateHolder()
        {
            
        }
    }
}
