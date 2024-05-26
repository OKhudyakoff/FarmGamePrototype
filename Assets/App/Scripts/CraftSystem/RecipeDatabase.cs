using System.Collections.Generic;
using UnityEngine;

public class RecipeDatabase : IService
{
    public List<RecipeData> AllRecipesList{get; private set;}

    public void LoadAllItems()
    {
        AllRecipesList = new List<RecipeData>(Resources.LoadAll<RecipeData>(""));
    }

    public void Init()
    {
        LoadAllItems();
    }
}
