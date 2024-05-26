using System;
using System.Collections.Generic;
using InventorySystem.Model;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/ItemSystem/Recipes/Recipe", fileName = "Recipe")]
public class RecipeData : ScriptableObject
{
    [SerializeField] private List<ItemContainer> _ingredients = new List<ItemContainer>();
    [SerializeField] private ItemContainer _result;

    public List<ItemContainer> Ingredients => _ingredients;
    public ItemContainer Result => _result;
}
