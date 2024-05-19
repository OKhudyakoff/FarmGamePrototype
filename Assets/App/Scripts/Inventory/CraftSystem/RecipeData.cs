using System;
using System.Collections.Generic;
using InventorySystem.Model;
using UnityEngine;

[CreateAssetMenu(menuName = "Inventory/Recipe")]
public class RecipeData : ScriptableObject
{
    [SerializeField] private List<Ingredient> _ingredients = new List<Ingredient>();
    [SerializeField] private ItemData _result;

    public List<Ingredient> Ingredients => _ingredients;
    public ItemData Result => _result;


    [Serializable]
    public class Ingredient
    {
        public ItemData Item;
        public int Amount;
    }
}
