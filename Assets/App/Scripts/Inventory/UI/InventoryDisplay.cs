using InventorySystem.Model;
using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem.UI
{
    public class InventoryDisplay : MonoBehaviour
    {
        [SerializeField] private InventorySlotDisplay _slotPrefab;
        [SerializeField] private Transform _slotsContainer;

        private List<InventorySlotDisplay> _slots = new List<InventorySlotDisplay>();
        private InventoryController _controller;
        private InventoryModel _inventory;

        public void Init(InventoryController controller, InventoryModel inventory)
        {
            _controller = controller;
            _inventory = inventory;

            int _slotsCount = _slots.Count;

            if (_slotsCount < inventory.Slots.Count)
            {
                for (int i = 0; i < inventory.Slots.Count - _slotsCount; i++)
                {
                    InventorySlotDisplay slotDisplay = Instantiate(_slotPrefab, _slotsContainer);
                    _slots.Add(slotDisplay);
                }
            }
            else if(_slotsCount > inventory.Slots.Count)
            {
                for(int i = 0; i < _slots.Count; i++)
                {
                    if(i > _inventory.Slots.Count)
                    {
                        _slots[i].gameObject.SetActive(false);
                    }
                    else
                    {
                        _slots[i].gameObject.SetActive(true);
                    }
                }
            }

            for (int i = 0; i < _slots.Count; i++)
            {
                _slots[i].Init(_inventory.Slots[i], _controller);
            }
        }
    }
}
