using InventorySystem.Model;
using InventorySystem.UI;
using UnityEngine;

namespace InventorySystem.Controllers
{
    public class PlayerInventory : InventoryController, IService
    {
        [SerializeField] private int inventorySize;
        [SerializeField] private InventoryDisplay _display;
        [SerializeField] private PlayerHotbar _hotbarInventory;

        public InventoryModel Model => _inventory;

        public override void Init()
        {
            _inventorySize = inventorySize;
            base.Init();
            _display.Init(this);
        }

        public override int AddItem(ItemData itemData, int count)
        {
            int _count = count;
            if(_connectedInventory != _hotbarInventory && _hotbarInventory != null)
            {
                _count = _hotbarInventory.AddItem(itemData, _count);
                if (_count > 0)
                {
                    _count = _inventory.AddItem(itemData, _count);
                    GameDebugger.ShowInfo($"{count - _count} {itemData.ItemName} удалено из {name}");
                    return _count;
                }
            }
            _count = _inventory.AddItem(itemData, _count);
            GameDebugger.ShowInfo($"{count - _count} {itemData.ItemName} добавлено в {name}");
            return _count;
        }

        public override void ConnectWithOtherInventory(InventoryController otherInventory = null)
        {
            if(otherInventory != null && otherInventory!= this && otherInventory != _hotbarInventory)
            {
                _connectedInventory = otherInventory;
                _hotbarInventory.ConnectWithOtherInventory(otherInventory);
            }
            else
            {
                _connectedInventory = _hotbarInventory;
                _hotbarInventory.ConnectWithOtherInventory(this);
            }
        }

        public override void DisconnectWithOtherInventory()
        {
            _connectedInventory = null;
            _hotbarInventory.ConnectWithOtherInventory(this);
        }
    }
}
