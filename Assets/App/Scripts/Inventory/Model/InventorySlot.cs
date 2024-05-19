using System;
using System.Diagnostics;

namespace InventorySystem.Model
{
    [System.Serializable]
    public class InventorySlot
    {
        public ItemData ItemData { get; private set; }
        public int StackSize { get; private set; }
        public Action OnSlotUpdated;

        public InventorySlot()
        {
            ItemData = null;
            StackSize = 0;
        }

        public void SetItem(ItemData itemData, int count = 1)
        {
            if(count > 0)
            {
                this.ItemData = itemData;
                StackSize = count;
                OnSlotUpdated?.Invoke();
            }
            else
            {
                ClearSlot();
            }
        }

        public void IncreaseQuantity(int countToAdd = 1)
        {
            StackSize += countToAdd;
            OnSlotUpdated?.Invoke();
        }

        public void DecreaseQuantity(int countToRemove = 1)
        {
            StackSize -= countToRemove;
            if (StackSize <= 0)
                ClearSlot();
            else
            {
                OnSlotUpdated?.Invoke();
            }
        }

        public void ClearSlot()
        {
            ItemData = null;
            StackSize = 0;
            OnSlotUpdated?.Invoke();
        }

        public bool IsEmpty => ItemData == null || StackSize <= 0;
    }
}
