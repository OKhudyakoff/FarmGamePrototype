using InteractionSystem;
using InventorySystem.Controllers;
using InventorySystem.Model;
using InventorySystem.UI;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Outline))]
public class CraftTable : InventoryController, IInteraction
{
    [SerializeField] private CraftTableWindow _craftWindow;
    [SerializeField] private InventoryDisplay _tableInventoryDisplay;
    [SerializeField] private InventoryDisplay _playerInventoryDisplay;
    [SerializeField] private int inventorySize = 9;
    private List<RecipeData> _recipeDatas;

    private CraftSystem _craftSystem;
    private bool _isOpened = false;
    private Outline _outline;
    private List<RecipeData> _resultRecipes = new List<RecipeData>();
    private int _currentRecipeIndex = 0;
    public int CurrentRecipeIndex { get {
        if(_currentRecipeIndex < _resultRecipes.Count)
        {
            return _currentRecipeIndex;
        }
        else
        {
            _currentRecipeIndex = 0;
            return _currentRecipeIndex;
        }
    }}
    private PlayerInventory _playerInventory;

    private void Start()
    {
        _recipeDatas = ServiceLocator.Current.Get<RecipeDatabase>().AllRecipesList;
        _craftSystem = new CraftSystem(this, _recipeDatas);
        
        _inventorySize = inventorySize;
        Init();
        _playerInventory = ServiceLocator.Current.Get<PlayerInventory>();
        _playerInventoryDisplay.Init(_playerInventory);
        _craftWindow.SetTable(this);

        _tableInventoryDisplay.Init(this);

        _outline = GetComponent<Outline>();
        _isOpened = false;
        _outline.enabled = false;
    }

    private void UpdateState()
    {
        if (_isOpened)
        {
            _playerInventory.ConnectWithOtherInventory(this);
            ConnectWithOtherInventory(_playerInventory);
            UpdateHolder();
            _craftWindow.Open();
        }
        else
        {
            _playerInventory.DisconnectWithOtherInventory();
            DisconnectWithOtherInventory();
            _craftWindow.Close();
        }
    }

    public override void UpdateHolder()
    {
        _resultRecipes = _craftSystem.GetAvaliableRecipes();
        _craftWindow.DisplayAvaliableRecipes(_resultRecipes);
    }

    public void ChangeCurrentRecipeIndex(int value)
    {
        if (value == 1)
        {
            _currentRecipeIndex++;
            if (CurrentRecipeIndex >= _resultRecipes.Count)
            {
                _currentRecipeIndex = 0;
            }
        }
        else if (value == -1)
        {
            _currentRecipeIndex--;
            if (CurrentRecipeIndex < 0)
            {
                _currentRecipeIndex = _resultRecipes.Count - 1;
            }
        }
        else _currentRecipeIndex = 0;
        _craftWindow.UpdateDisplay();
    }

    public void Craft()
    {
        if(_resultRecipes.Count > 0)
        {
            ItemContainer craftedItem = _craftSystem.Craft(_resultRecipes[CurrentRecipeIndex]);
            _playerInventory.AddItem(craftedItem.Item, craftedItem.Amount);
        }
    }

    #region Interaction
    public void Interact(Interactor interactor)
    {
        _isOpened = !_isOpened;
        UpdateState();
    }

    public void SelectObject()
    {
        _outline.enabled = true;
    }

    public void DeselectObject()
    {
        _isOpened = false;
        UpdateState();
        _outline.enabled = false;
    }
    #endregion
}
