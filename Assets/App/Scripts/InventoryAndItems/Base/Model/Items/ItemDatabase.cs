using System.Collections.Generic;
using InventorySystem.Model;
using UnityEngine;

public class ItemDatabase: IService
{
    public List<ItemData> AllItemsList{get; private set;}

    public void LoadAllItems()
    {
        AllItemsList = new List<ItemData>(Resources.LoadAll<ItemData>(""));
    }

    public void Init()
    {
        LoadAllItems();
    }
}

