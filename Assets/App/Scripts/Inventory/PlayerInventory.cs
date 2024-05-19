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

        private void Awake()
        {
            Init();
        }

        public override void Init()
        {
            _inventorySize = inventorySize;
            base.Init();
            _display.Init(this);
        }

        public override int AddItem(ItemData itemData, int count)
        {
            if(_otherInventory != _hotbar)
            {
                count = _hotbar.AddItem(itemData, count);
                if (count > 0)
                {
                    return _inventory.AddItem(itemData, count);
                }
                
            }
            return _inventory.AddItem(itemData, count);
        }

        public void ConnectWithHootbar()
        {
            SetOtherInventory(_hotbar);
        }
    }
}
