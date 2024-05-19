using InventorySystem.Model;
using InventorySystem.UI;
using UnityEngine;

namespace InventorySystem
{
    public class PlayerInventory : InventoryController, IService
    {
        [SerializeField] private int inventorySize;
        [SerializeField] private InventoryDisplay _display;
        [SerializeField] private PlayerHotbar _hotbar;

        public InventoryModel Model => _inventory;

        public override void Init()
        {
            base._inventorySize = inventorySize;
            _inventory = new InventoryModel(base._inventorySize);
            _display.Init(this, _inventory);
        }

        public override int AddItem(ItemData itemData, int count)
        {
            count = _hotbar.AddItem(itemData, count);
            if (count > 0) { _inventory.AddItem(itemData, count); }
            return count;
        }
    }
}
