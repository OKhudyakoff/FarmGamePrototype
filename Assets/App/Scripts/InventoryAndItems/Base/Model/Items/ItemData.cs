using System;
using UnityEngine;
namespace InventorySystem.Model
{
    public abstract class ItemData : ScriptableObject
    {
        [SerializeField] private Sprite _itemSprite;
        [SerializeField] private string _itemName;
        [SerializeField] private string _itemDescription;
        [SerializeField] private int _maxStackSize;
        [SerializeField] private GameObject _itemPrefab;

        public Sprite ItemSprite => _itemSprite;
        public string ItemName => _itemName;
        public string ItemDescription => _itemDescription;
        public int MaxStackSize => _maxStackSize;
        public GameObject ItemPrefab => _itemPrefab;
    }

    [Serializable]
    public class ItemContainer
    {
        public ItemData Item;
        public int Amount;
    }
}
