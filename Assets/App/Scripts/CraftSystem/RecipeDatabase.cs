using System.Collections.Generic;
using UnityEditor;

public class RecipeDatabase : IService
{
    public List<RecipeData> AllRecipesList{get; private set;}

    public void LoadAllItems()
    {
        AllRecipesList = new List<RecipeData>();

        string[] guids = AssetDatabase.FindAssets("t:RecipeData");
        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            RecipeData item = AssetDatabase.LoadAssetAtPath<RecipeData>(path);
            AllRecipesList.Add(item);
        }
    }

    public void Init()
    {
        LoadAllItems();
    }
}
