using InventorySystem;
using InventorySystem.Model;
using System.Collections.Generic;
using UnityEngine;

public class InventoryTester : MonoBehaviour
{
    [SerializeField] private List<StartItem> items = new List<StartItem>();
    [SerializeField] private InventoryController inventoryController;

    private void Start()
    {
        for (int i = 0; i<items.Count; i++)
        {
            inventoryController.AddItem(items[i].Item, items[i].Amount);
        }
    }

    [System.Serializable]
    private class StartItem
    {
        public ItemData Item;
        public int Amount;
    }
}
