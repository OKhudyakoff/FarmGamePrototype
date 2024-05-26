using InventorySystem.Controllers;
using InventorySystem.Model;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryTester : MonoBehaviour
{
    [SerializeField] private List<StartItem> _startItems = new List<StartItem>();
    [SerializeField] private InventoryController inventoryController;

    [SerializeField] private Button rndAddButton;
    [SerializeField] private Button rndRemoveButton;

    private ItemDatabase _itemDatabase;
    private System.Random rnd;

    private void Start()
    {
        _itemDatabase = ServiceLocator.Current.Get<ItemDatabase>();
        rnd = new System.Random();
        for (int i = 0; i< _startItems.Count; i++)
        {
            inventoryController.AddItem(_startItems[i].Item, _startItems[i].Amount);
        }


        rndAddButton.onClick.AddListener(AddRandItem);
        rndRemoveButton.onClick.AddListener(RemoveRandItem);
    }

    private void AddRandItem()
    {
        int itemID = rnd.Next(0, _itemDatabase.AllItemsList.Count);
        int countToAdd = rnd.Next(1, _itemDatabase.AllItemsList[itemID].MaxStackSize);
        inventoryController.AddItem(_itemDatabase.AllItemsList[itemID], countToAdd);
    }

    private void RemoveRandItem()
    {
        List<ItemData> lst = inventoryController.ItemsInInventory();
        if(lst.Count > 0)
        {
            int itemID = rnd.Next(0, lst.Count);
            int countToRemove = rnd.Next(1, inventoryController.ItemCount(lst[itemID]));
            Debug.Log(lst[itemID].ItemName + " " + countToRemove);
            inventoryController.RemoveItem(lst[itemID], countToRemove);
        }
    }

    [System.Serializable]
    private class StartItem
    {
        public ItemData Item;
        public int Amount;
    }
}
