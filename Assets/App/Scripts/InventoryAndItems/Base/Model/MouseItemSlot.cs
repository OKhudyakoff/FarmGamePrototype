using InventorySystem.Model;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace InventorySystem.UI
{
    public class MouseItemSlot : MonoBehaviour, IService
    {
        [SerializeField] private Image itemImage;
        [SerializeField] private TMP_Text amountText;
        public InventorySlot Slot { get; private set; }

        public void Init()
        {
            Slot = new InventorySlot();
            Slot.OnSlotUpdated += UpdateDisplay;
            UpdateDisplay();
        }

        public void Set(InventorySlot slot)
        {
            Slot.SetItem(slot.ItemData, slot.StackSize);
            slot.ClearSlot();
        }

        public void SplitStack(InventorySlot otherSlot)
        {
            if(otherSlot.StackSize - otherSlot.StackSize/2 > 0)
            {
                Slot.SetItem(otherSlot.ItemData, otherSlot.StackSize / 2);
                otherSlot.DecreaseQuantity(otherSlot.StackSize / 2);
            }
            else
            {
                Set(otherSlot);
            }
        }

        private void UpdateDisplay()
        {
            if(Slot != null && !Slot.IsEmpty)
            {
                itemImage.gameObject.SetActive(true);
                amountText.gameObject.SetActive(true);
                itemImage.sprite = Slot.ItemData.ItemSprite;
                amountText.text = Slot.StackSize.ToString();
            }
            else
            {
                itemImage.gameObject.SetActive(false);
                amountText.gameObject.SetActive(false);
            }
        }

        public void Drop()
        {
            if (Slot != null)
            {
                Slot.ClearSlot();
            }
        }

        public bool IsEmpty => Slot == null || Slot.IsEmpty;

        private void OnDestroy()
        {
            if(Slot != null)
                Slot.OnSlotUpdated -= UpdateDisplay;
        }
    }
}
