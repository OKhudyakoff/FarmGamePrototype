using System.Collections.Generic;
using System.Diagnostics;

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

        public bool IsContain(ItemData item, int count)
        {
            int amount = 0;
            foreach (InventorySlot slot in Slots)
            {
                if(slot.ItemData == item)
                {
                    amount += slot.StackSize;
                }
            }
            return amount >= count;
        }

        public int GetItemCount(ItemData item)
        {
            int amount = 0;
            for (int i = 0; i < Slots.Count; i++)
            {
                if (Slots[i].ItemData == item)
                {
                    amount += Slots[i].StackSize;
                }
            }
            return amount;
        }

        public void RemoveItem(ItemData item, int count)
        {
            foreach (InventorySlot slot in Slots)
            {
                if(count > 0)
                {
                    if (slot.ItemData == item)
                    {
                        if (count > slot.StackSize)
                        {
                            count -= slot.StackSize;
                            slot.ClearSlot();
                        }
                        else
                        {
                            slot.RemoveItem(count);
                            count = 0;
                        }
                    }
                }
                else
                {
                    return;
                }
            }
        }
    }
}
