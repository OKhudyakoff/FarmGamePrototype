using InventorySystem.Model;
using InventorySystem.UI;
using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem.Controllers
{
    public abstract class InventoryController : MonoBehaviour, ISlotHolder
    {
        [SerializeField] private bool playerHaveAccess;
        protected int _inventorySize;
        protected InventoryModel _inventory;
        protected InputHandler _inputHandler;
        protected MouseItemSlot _mouseSlot;
        protected InventoryController _connectedInventory;

        protected bool IsSplitting => _inputHandler.IsSplitting;

        public virtual void Init()
        {
            _inventory = new InventoryModel(_inventorySize);
            _inputHandler = ServiceLocator.Current.Get<InputHandler>();
            _mouseSlot = ServiceLocator.Current.Get<MouseItemSlot>();
        }

        public virtual int AddItem(ItemData itemData, int count)
        {
            if(count <= 0) return 0;
            UpdateHolder();
            int _count = _inventory.AddItem(itemData, count);
            GameDebugger.ShowInfo($"{count - _count} {itemData.ItemName} добавлено в {name}");
            return _count;
        }

        public void RemoveItem(ItemData itemData, int count)
        {
            if (_inventory.ItemContainedCount(itemData) >= count)
            {
                _inventory.RemoveItem(itemData, count);
                GameDebugger.ShowInfo($"{count} {itemData.ItemName} удалено из {name}");
            }
        }

        public virtual void OnLeftBtnSlotClicked(InventorySlotDisplay slot)
        {
            if (!playerHaveAccess) return;

            if (!slot.InvSlot.IsEmpty)
            {
                HandleNonEmptySlotLeftClick(slot, _mouseSlot);
            }
            else if (!_mouseSlot.IsEmpty)
            {
                slot.InvSlot.SetItem(_mouseSlot.Slot.ItemData, _mouseSlot.Slot.StackSize);
                _mouseSlot.Slot.DecreaseQuantity(_mouseSlot.Slot.StackSize);
            }
        }

        private void HandleNonEmptySlotLeftClick(InventorySlotDisplay slot, MouseItemSlot mouseSlot)
        {
            if (mouseSlot.IsEmpty)
            {
                if (!IsSplitting)
                {
                    mouseSlot.Set(slot.InvSlot);
                }
                else if (_connectedInventory != null)
                {
                    int amountRemaining = _connectedInventory.AddItem(slot.InvSlot.ItemData, slot.InvSlot.StackSize);
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

            if (_mouseSlot.IsEmpty && !slot.InvSlot.IsEmpty)
            {
                _mouseSlot.SplitStack(slot.InvSlot);
            }
            else if (!_mouseSlot.IsEmpty)
            {
                HandleRightClickWithMouseSlot(slot, _mouseSlot);
            }
        }

        private void HandleRightClickWithMouseSlot(InventorySlotDisplay slot, MouseItemSlot mouseSlot)
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

        public virtual void ConnectWithOtherInventory(InventoryController otherInventory = null)
        {
            _connectedInventory = otherInventory;
        }

        public virtual void DisconnectWithOtherInventory()
        {
            _connectedInventory = null;
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
