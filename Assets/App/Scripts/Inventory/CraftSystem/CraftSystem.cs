using InventorySystem;
using InventorySystem.Model;
using InventorySystem.UI;
using System;
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
        PlayerInventory pInventory = ServiceLocator.Current.Get<PlayerInventory>();
        _inventorySize = 9;
        Init();
        _tableInventoryDisplay.Init(this, _inventory);
        _playerInventoryDisplay.Init(pInventory, pInventory.Model);
        UpdateRecipe();
    }

    public override void OnLeftBtnSlotClicked(InventorySlotDisplay slot)
    {
        base.OnLeftBtnSlotClicked(slot);
        UpdateRecipe();
    }

    private void UpdateRecipe()
    {
        foreach (var recipe in _recipeDatas)
        {
            bool isCanCraft = true;
            foreach (var ingredient in recipe.Ingredients)
            {
                if(_inventory.ItemContainedCount(ingredient.Item) < ingredient.Amount)
                {
                    Debug.Log(isCanCraft);
                    isCanCraft = false;
                }
            }
            if(isCanCraft)
            {
                Debug.Log("Success");
                recipeInfo.gameObject.SetActive(true);
                recipeInfo.Init(recipe.Result,1);
                break;
            }
            else
            {
                recipeInfo.gameObject.SetActive(false);
            }
        }
    }

    public override int AddItem(ItemData itemData, int count)
    {
        return 0;
    }
}
