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

        private bool _isClicked;
        public InventorySlot InvSlot { get; private set; }

        public void Init(InventorySlot slot, ISlotHolder controller)
        {
            RemoveSelection();
            _slotHolder = controller;
            InvSlot = slot;
            InvSlot.OnSlotUpdated += UpdateSlot;
            UpdateSlot();
        }

        private void UpdateSlot()
        {
            if (!InvSlot.IsEmpty)
            {
                itemImage.sprite = InvSlot.ItemData.ItemSprite;
                amountText.text = InvSlot.StackSize.ToString();
                itemImage.gameObject.SetActive(true);
                amountText.gameObject.SetActive(true);
            }
            else
            {
                HideUI();
            }
            _slotHolder.UpdateHolder();
        }

        private void HideUI()
        {
            itemImage.gameObject.SetActive(false);
            amountText.gameObject.SetActive(false);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _isClicked = true;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if(_isClicked)
            {
                OnClicked(eventData.button == PointerEventData.InputButton.Right);
            }
        }

        public void OnDrop(PointerEventData eventData)
        {
            OnClicked(false);
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (_isClicked)
            {
                OnClicked(true);
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
            ServiceLocator.Current.Get<ToolTipController>().Hide();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (InvSlot != null && !InvSlot.IsEmpty)
            {
                ServiceLocator.Current.Get<ToolTipController>().Show(InvSlot.ItemData.ItemDescription, InvSlot.ItemData.ItemName, InvSlot.ItemData.ItemSprite);
            }
        }

        private void OnClicked(bool isRight)
        {
            if(isRight) _slotHolder.OnRightBtnSlotClicked(this);
            else _slotHolder.OnLeftBtnSlotClicked(this);
            _isClicked = false;
        }

        private void OnDestroy()
        {
            if (InvSlot != null)
            {
                InvSlot.OnSlotUpdated -= UpdateSlot;
            }
        }
    }
}
