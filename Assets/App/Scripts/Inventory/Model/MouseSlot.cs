using InventorySystem.Model;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace InventorySystem.UI
{
    public class MouseSlot : MonoBehaviour
    {
        [SerializeField] private Image itemImage;
        [SerializeField] private TMP_Text amountText;
        public InventorySlot Slot { get; private set; }

        private void Start()
        {
            Slot = new InventorySlot();
            UpdateDisplay();
        }

        public void Set(InventorySlot slot)
        {
            if(Slot != null)
            {
                Slot.OnSlotUpdated -= UpdateDisplay;
            }
            Slot = slot;
            Slot.IsMouseSlot = true;
            Slot.OnSlotUpdated += UpdateDisplay;
            UpdateDisplay();
        }

        public void SplitStack(InventorySlot otherSlot)
        {
            if(otherSlot.StackSize - otherSlot.StackSize/2 > 0)
            {
                Slot = new InventorySlot();
                Slot.OnSlotUpdated += UpdateDisplay;
                Slot.SetItem(otherSlot.ItemData, otherSlot.StackSize / 2);
                otherSlot.RemoveItem(otherSlot.StackSize / 2);
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
                ClearSlot();
            }
        }

        public void ClearSlot()
        {
            if (Slot != null)
            {
                Slot.IsMouseSlot = false;
                Slot.OnSlotUpdated -= UpdateDisplay;
                Slot = new InventorySlot();
                itemImage.gameObject.SetActive(false);
                amountText.gameObject.SetActive(false);
            }
        }

        public bool IsEmpty => Slot.IsEmpty;

        private void OnDestroy()
        {
            Slot.OnSlotUpdated -= UpdateDisplay;
        }
    }
}
