using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftTableWindow : BaseWindow
{
    [SerializeField] private RecipeInfo _resultInfo;
    [SerializeField] private Button _nextBtn;
    [SerializeField] private Button _prevBtn;
    [SerializeField] private Button _craftBtn;

    private CraftTable _craftTable;
    private List<RecipeData> _recipesToDisplay = new List<RecipeData>();
    
    public void SetTable(CraftTable craftTable)
    {
        Init();
        _craftTable = craftTable;
        _nextBtn.onClick.AddListener(() => _craftTable.ChangeCurrentRecipeIndex(1));
        _prevBtn.onClick.AddListener(() => _craftTable.ChangeCurrentRecipeIndex(-1));
        _craftBtn.onClick.AddListener(Craft);

        _nextBtn.gameObject.SetActive(false);
        _prevBtn.gameObject.SetActive(false);

        _craftTable.ChangeCurrentRecipeIndex(0);
    }

    private void Craft()
    {
        _craftTable.Craft();
    }

    public void DisplayAvaliableRecipes(List<RecipeData> recipesToDisplay)
    {
        _recipesToDisplay = recipesToDisplay;
        UpdateDisplay();
    }

    public void UpdateDisplay()
    {
        _prevBtn.gameObject.SetActive(_recipesToDisplay.Count > 1 ? true : false);
        _nextBtn.gameObject.SetActive(_recipesToDisplay.Count > 1 ? true : false);
        if (_recipesToDisplay.Count >= 1)
        {
            _craftBtn.gameObject.SetActive(true);
            _resultInfo.gameObject.SetActive(true);
            _resultInfo.Init(_recipesToDisplay[_craftTable.CurrentRecipeIndex].Result.Item, _recipesToDisplay[_craftTable.CurrentRecipeIndex].Result.Amount);
        }
        else
        {
            _craftBtn.gameObject.SetActive(false);
            _resultInfo.gameObject.SetActive(false);
        }
    }
}
