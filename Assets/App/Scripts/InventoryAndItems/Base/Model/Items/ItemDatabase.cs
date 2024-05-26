using System.Collections.Generic;
using InventorySystem.Model;
using UnityEditor;

public class ItemDatabase: IService
{
    public List<ItemData> AllItemsList{get; private set;}

    public void LoadAllItems()
    {
        AllItemsList = new List<ItemData>();

        string[] guids = AssetDatabase.FindAssets("t:ItemData");
        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            ItemData item = AssetDatabase.LoadAssetAtPath<ItemData>(path);
            AllItemsList.Add(item);
        }
    }

    public void Init()
    {
        LoadAllItems();
    }
}

