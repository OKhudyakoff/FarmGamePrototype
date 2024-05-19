using InventorySystem.Model;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RecipeInfo : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Image _image; 
    [SerializeField] private TMP_Text _amountText;
    private ItemData _itemData;

    public void Init(ItemData itemData, int amount)
    {
        _itemData = itemData;
        _amountText.text = amount.ToString();
        _image.sprite = itemData.ItemSprite;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_itemData != null)
        {
            ServiceLocator.Current.Get<ToolTip>().Show(_itemData.ItemName, _itemData.ItemDescription, _itemData.ItemSprite);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ServiceLocator.Current.Get<ToolTip>().Hide();
    }
}
