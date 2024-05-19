using TMPro;
using UnityEngine;

public class CraftRecipeObjectUI : MonoBehaviour
{
    [SerializeField] private RecipeInfo _recipeInfoPrefab;
    [SerializeField] private TMP_Text _signTMP;

    public void SetRecipe(RecipeData recipe)
    {
        if(recipe != null)
        {
            for(int i = 0; i < recipe.Ingredients.Count; i++)
            {
                RecipeInfo info = Instantiate(_recipeInfoPrefab, this.transform);
                info.Init(recipe.Ingredients[i].Item, recipe.Ingredients[i].Amount);
                TMP_Text tmp = Instantiate(_signTMP, this.transform);
                tmp.text = i <= recipe.Ingredients.Count - 2 ? "+" : "=";
            }
            RecipeInfo result = Instantiate(_recipeInfoPrefab, this.transform);
            result.Init(recipe.Result, 1);
        }
    }
}
