using InventorySystem.Model;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace InventorySystem.UI
{
    public class InventorySlotDisplay : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler, IDropHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private Image itemImage;
        [SerializeField] private TMP_Text amountText;
        [SerializeField] private GameObject selection;
        public InventorySlot InvSlot { get; private set; }
        private InventoryController _inventoryController;
        private bool click;

        public void Init(InventorySlot slot, InventoryController controller)
        {
            RemoveSelection();
            _inventoryController = controller;
            if (InvSlot != null) InvSlot.OnSlotUpdated -= UpdateSlot;
            if(slot != null)
            {
                InvSlot = slot;
                InvSlot.OnSlotUpdated += UpdateSlot;
                UpdateSlot();
            }
            else
            {
                HideUI();
            }
        }

        public void UpdateSlot()
        {
            if(InvSlot != null && !InvSlot.IsEmpty)
            {
                if(!InvSlot.IsMouseSlot)
                {
                    ShowUI();
                }
                itemImage.sprite = InvSlot.ItemData.ItemSprite;
                amountText.text = InvSlot.StackSize.ToString();
            }
            else
            {
                HideUI();
            }
        }

        public void HideUI()
        {
            itemImage.gameObject.SetActive(false);
            amountText.gameObject.SetActive(false);
        }

        public void ShowUI()
        {
            if(!InvSlot.IsEmpty)
            {
                itemImage.gameObject.SetActive(true);
                amountText.gameObject.SetActive(true);
            }
            else 
            { 
                HideUI(); 
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            click = true;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if(eventData.button == PointerEventData.InputButton.Right)
            {
                OnRightClick();
            }
            else if (click)
            {
                OnLeftClick();
            }
        }

        public void OnDrop(PointerEventData eventData)
        {
            OnLeftClick();
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (click)
            {
                OnLeftClick();
            }
        }

        private void OnLeftClick()
        {
            click = false;
            _inventoryController.OnSlotLeftClicked(this);
        }

        private void OnRightClick()
        {
            _inventoryController.OnSlotRightClicked(this);
        }

        public void SetSelection()
        {
            selection.SetActive(true);
        }

        public void RemoveSelection()
        {
            selection.SetActive(false);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _inventoryController.OnPointerExit();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _inventoryController.OnPointerEnter(InvSlot);
        }
    }
}
