using InventorySystem.UI;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropItemZone : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private bool _isClicked = false;
    private MouseItemSlot _mouseSlot;

    public void OnPointerDown(PointerEventData eventData)
        {
            _isClicked = true;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            OnClicked(eventData.button == PointerEventData.InputButton.Right);
        }

    private void OnClicked(bool isRight)
    {
        if(_mouseSlot!= null && !_mouseSlot.IsEmpty && _isClicked)
        {
            if(!isRight) _mouseSlot.Drop();
            else _mouseSlot.Slot.DecreaseQuantity();
            _isClicked = false;
        }
    }
}
