using System;

namespace InventorySystem.Model
{
    [System.Serializable]
    public class InventorySlot
    {
        public ItemData ItemData { get; private set; }
        public int StackSize { get; private set; }
        public Action OnSlotUpdated;
        public bool IsLockedToDisplay;

        public InventorySlot()
        {
            ItemData = null;
            StackSize = 0;
            IsLockedToDisplay = false;
        }

        public void SetItem(ItemData itemData, int count = 1, bool isLockedToDisplay = false)
        {
            this.ItemData = itemData;
            StackSize = count;
            IsLockedToDisplay = isLockedToDisplay;
            OnSlotUpdated?.Invoke();
        }

        public void IncreaseQuantity(int countToAdd)
        {
            StackSize += countToAdd;
            OnSlotUpdated?.Invoke();
        }

        public void DecreaseQuantity(int countToRemove)
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
            IsLockedToDisplay = false;
            OnSlotUpdated?.Invoke();
        }

        public bool IsEmpty => ItemData == null || StackSize <= 0;
    }
}
