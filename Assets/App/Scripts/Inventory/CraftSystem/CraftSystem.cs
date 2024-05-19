using InventorySystem;
using InventorySystem.Model;
using InventorySystem.UI;
using System.Collections.Generic;
using UnityEngine;

public class CraftSystem : InventoryController
{
    [SerializeField] private List<RecipeData> _recipeDatas = new List<RecipeData>();
    [SerializeField] private InventoryDisplay _tableInventoryDisplay;
    [SerializeField] private InventoryDisplay _playerInventoryDisplay;
    [SerializeField] private RecipeInfo recipeInfo;

    private void Start()
    {
        _inventorySize = 9;
        Init();
        PlayerInventory pInventory = ServiceLocator.Current.Get<PlayerInventory>();
        _tableInventoryDisplay.Init(this);
        _playerInventoryDisplay.Init(pInventory);
        UpdateRecipe();
    }

    public void UpdateConnectionWithPlayerInventory(bool isConnected)
    {
        var playerInventory = ServiceLocator.Current.Get<PlayerInventory>();
        if (isConnected)
        {
            playerInventory.SetOtherInventory(this);
            SetOtherInventory(playerInventory);
        }
        else
        {
            playerInventory.SetOtherInventory(null);
            SetOtherInventory(null);
        }
    }

    private void UpdateRecipe()
    {
        Debug.Log("RecipeUpdated");
        foreach (var recipe in _recipeDatas)
        {
            bool isCanCraft = CanCraft(recipe);
            if (isCanCraft)
            {
                DisplayRecipeInfo(recipe);
                break;
            }
            else
            {
                recipeInfo.gameObject.SetActive(false);
            }
        }
    }

    public override void UpdateHolder()
    {
        UpdateRecipe();
    }

    private bool CanCraft(RecipeData recipe)
    {
        foreach (var ingredient in recipe.Ingredients)
        {
            if (_inventory.ItemContainedCount(ingredient.Item) < ingredient.Amount)
            {
                return false;
            }
        }
        return true;
    }

    private void DisplayRecipeInfo(RecipeData recipe)
    {
        recipeInfo.gameObject.SetActive(true);
        recipeInfo.Init(recipe.Result, 1);
    }

    public override int AddItem(ItemData itemData, int count)
    {
        int ammountToReturn = _inventory.AddItem(itemData, count);
        UpdateRecipe();
        return ammountToReturn;
    }
}
