using InventorySystem.Model;
using InventorySystem.UI;
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

        public void OnSlotLeftClicked(InventorySlotDisplay clickedSlotDisplay)
        {
            MouseSlot _mouseSlot = ServiceLocator.Current.Get<Mouse>().MouseSlot;
            if (playerHaveAccess)
            {
                // Слот не пуст
                if (!clickedSlotDisplay.InvSlot.IsEmpty)
                {
                    //Мышь пуста
                    if (_mouseSlot.IsEmpty)
                    {
                        clickedSlotDisplay.HideUI();
                        _mouseSlot.Set(clickedSlotDisplay.InvSlot);
                    }
                    else
                    {
                        // Кликнули на тот же самый слот
                        if (_mouseSlot.Slot == clickedSlotDisplay.InvSlot)
                        {
                            _mouseSlot.ClearSlot();
                        }
                        // Кликнули на такой же предмет
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
    }
}
