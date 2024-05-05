using System.Collections.Generic;

namespace InventorySystem.Model
{
    public class InventoryModel
    {
        public List<InventorySlot> Slots { get; private set; }
        public InventoryModel(int inventorySize)
        {
            Slots = new List<InventorySlot>();
            for (int i = 0; i < inventorySize; i++)
            {
                InventorySlot slot = new InventorySlot();
                Slots.Add(slot);
            }
        }

        public int AddItem(ItemData item, int count)
        {
            foreach (InventorySlot slot in Slots)
            {
                if (slot.ItemData == item)
                {
                    if (count > slot.ItemData.MaxStackSize - slot.StackSize)
                    {
                        count -= slot.ItemData.MaxStackSize - slot.StackSize;
                        slot.AddItem(slot.ItemData.MaxStackSize - slot.StackSize);
                    }
                    else
                    {
                        slot.AddItem(count);
                        return 0;
                    }
                }
            }
            //Fill empty slots
            foreach (InventorySlot slot in Slots)
            {
                if (slot.IsEmpty)
                {
                    if (count > item.MaxStackSize)
                    {
                        count -= item.MaxStackSize;
                        slot.SetItem(item, item.MaxStackSize);
                    }
                    else
                    {
                        slot.SetItem(item, count);
                        return 0;
                    }
                }
            }
            return count;
        }

    }
}
