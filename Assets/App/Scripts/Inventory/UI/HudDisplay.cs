using InventorySystem.Model;
using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem.UI
{
    public class HudDisplay : MonoBehaviour
    {
        public List<InventorySlotDisplay> Slots => _slots;
        [SerializeField] private List<InventorySlotDisplay> _slots = new List<InventorySlotDisplay>();

        private InventoryController _controller;
        private InventoryModel _model;

        public void Init(InventoryController controller, InventoryModel model)
        {
            _controller = controller;
            _model = model;
            for (int i = 0; i < _slots.Count; i++)
            {
                _slots[i].Init(_model.Slots[i], _controller);
            }
        }
    }
}
