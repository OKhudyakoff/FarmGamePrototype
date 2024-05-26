using System.Collections.Generic;
using InventorySystem.Model;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(RecipeData))]
public class RecipeDataEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // Получаем ссылку на редактируемый объект
        RecipeData recipeData = (RecipeData)target;

        // Отображаем стандартный инспектор
        DrawDefaultInspector();

        // Добавляем кнопку для объединения дубликатов
        if (GUILayout.Button("Combine Duplicate Ingredients"))
        {
            CombineDuplicateIngredients(recipeData);
        }
    }

    private void CombineDuplicateIngredients(RecipeData recipeData)
    {
        Dictionary<ItemData, int> ingredientDict = new Dictionary<ItemData, int>();

        foreach (var ingredient in recipeData.Ingredients)
        {
            if (ingredientDict.ContainsKey(ingredient.Item))
            {
                ingredientDict[ingredient.Item] += ingredient.Amount;
            }
            else
            {
                ingredientDict[ingredient.Item] = ingredient.Amount;
            }
        }

        if (ingredientDict.Count != recipeData.Ingredients.Count)
        {
            // Если были дубликаты и они были объединены, обновляем список ингредиентов
            recipeData.Ingredients.Clear();
            foreach (var entry in ingredientDict)
            {
                recipeData.Ingredients.Add(new ItemContainer
                {
                    Item = entry.Key,
                    Amount = entry.Value
                });
            }

            // Сохраняем изменения в объекте
            EditorUtility.SetDirty(recipeData);

            // Обновляем инспектор для отображения изменений
            serializedObject.ApplyModifiedProperties();
        }
    }
}
