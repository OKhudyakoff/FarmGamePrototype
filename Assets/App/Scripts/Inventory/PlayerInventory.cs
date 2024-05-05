using InventorySystem.Model;
using InventorySystem.UI;
using UnityEngine;

namespace InventorySystem
{
    public class PlayerInventory : InventoryController, IService
    {
        [SerializeField] private int inventorySize;
        [SerializeField] private InventoryDisplay _display;
        [SerializeField] private GameObject _panel;
        [SerializeField] private PlayerHotbar _hotbar;
        private bool isInventoryOpened;

        public override void Init()
        {
            base._inventorySize = inventorySize;
            _inventory = new InventoryModel(base._inventorySize);
            _display.Init(this, _inventory);
        }

        public void TriggerInventory()
        {

            if (!isInventoryOpened)
            {
                isInventoryOpened = true;
                _panel.SetActive(true);
                UpdateDisplay();
                ServiceLocator.Current.Get<Mouse>().UnlockCursor();
            }
            else
            {
                ServiceLocator.Current.Get<Mouse>().MouseSlot.ClearSlot();
                UpdateDisplay();
                isInventoryOpened = false;
                _panel.SetActive(false);
                ServiceLocator.Current.Get<Mouse>().LockCursor();
            }
        }

        public override int AddItem(ItemData itemData, int count)
        {
            count = _hotbar.AddItem(itemData, count);
            if (count > 0) { _inventory.AddItem(itemData, count); }
            return count;
        }

        public override void UpdateDisplay()
        {
            _display.UpdateSlotDisplays();
        }
    }
}
