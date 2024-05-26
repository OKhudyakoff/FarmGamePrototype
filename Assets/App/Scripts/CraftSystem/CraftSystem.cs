using InventorySystem.Controllers;
using InventorySystem.Model;
using System.Collections.Generic;

public class CraftSystem
{
    private List<RecipeData> _resultList = new List<RecipeData>();
    private List<RecipeData> _recipeDatas = new List<RecipeData>();
    private InventoryController _inventoryController;
    public bool IsCrafting { get; private set; }

    public CraftSystem(InventoryController inventoryController, List<RecipeData> recipeDatas)
    {
        IsCrafting = false;
        _inventoryController = inventoryController;
        _recipeDatas = recipeDatas;
    }

    public List<RecipeData> GetAvaliableRecipes()
    {
        _resultList.Clear();

        foreach (var recipe in _recipeDatas)
        {
            bool isCanCraft = IsCanCraft(recipe);
            if (isCanCraft)
            {
                _resultList.Add(recipe);
            }
        }
        return _resultList;
    }

    private bool IsCanCraft(RecipeData recipe)
    {
        foreach (var ingredient in recipe.Ingredients)
        {
            if (_inventoryController.GetInventory().ItemContainedCount(ingredient.Item) < ingredient.Amount)
            {
                return false;
            }
        }
        return true;
    }

    public ItemContainer Craft(RecipeData recipe)
    {
        IsCrafting = true;
        foreach (var itemData in recipe.Ingredients)
        {
            _inventoryController.RemoveItem(itemData.Item, itemData.Amount);
        }
        IsCrafting = false;
        return recipe.Result;
    }
}
