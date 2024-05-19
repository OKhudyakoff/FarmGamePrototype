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
        private ISlotHolder _slotHolder;

        private bool click;
        public InventorySlot InvSlot { get; private set; }

        public void Init(InventorySlot slot, ISlotHolder controller)
        {
            RemoveSelection();
            _slotHolder = controller;
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
            if(!InvSlot.IsEmpty)
            {
                itemImage.sprite = InvSlot.ItemData.ItemSprite;
                amountText.text = InvSlot.StackSize.ToString();
                UpdateVisualState();
            }
            else
            {
                HideUI();
            }
        }

        private void UpdateVisualState()
        {
            if (!InvSlot.IsLockedToDisplay)
            {
                itemImage.gameObject.SetActive(true);
                amountText.gameObject.SetActive(true);
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
            ServiceLocator.Current.Get<ToolTip>().Hide();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if(InvSlot != null && !InvSlot.IsEmpty)
                ServiceLocator.Current.Get<ToolTip>().Show(InvSlot.ItemData.ItemDescription, InvSlot.ItemData.ItemName, InvSlot.ItemData.ItemSprite);
        }

        private void OnLeftClick()
        {
            click = false;
            _slotHolder.OnLeftBtnSlotClicked(this);
        }

        private void OnRightClick()
        {
            _slotHolder.OnRightBtnSlotClicked(this);
        }

        private void OnDestroy()
        {
            InvSlot.OnSlotUpdated -= UpdateSlot;
        }
    }
}
