using System;

namespace InventorySystem.Model
{
    [System.Serializable]
    public class InventorySlot
    {
        public ItemData ItemData { get; private set; }
        private int _stackSize = 0;
        public int StackSize => _stackSize;
        public Action OnSlotUpdated;
        public bool IsMouseSlot;

        public InventorySlot()
        {
            ItemData = null;
            _stackSize = 0;
            IsMouseSlot = false;
        }

        public void SetItem(ItemData itemData, int count)
        {
            this.ItemData = itemData;
            _stackSize = count;
            OnSlotUpdated?.Invoke();
        }

        public void AddItem(int countToAdd)
        {
            _stackSize += countToAdd;
            OnSlotUpdated?.Invoke();
        }

        public void RemoveItem(int countToRemove)
        {
            _stackSize -= countToRemove;
            if (_stackSize <= 0)
                ClearSlot();
            else
            {
                OnSlotUpdated?.Invoke();
            }
        }

        public void ClearSlot()
        {
            ItemData = null;
            _stackSize = 0;
            OnSlotUpdated?.Invoke();
        }

        public bool IsEmpty => ItemData == null || StackSize <= 0;
    }
}
