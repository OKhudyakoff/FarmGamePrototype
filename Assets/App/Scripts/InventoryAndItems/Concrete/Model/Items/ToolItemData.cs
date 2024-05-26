using System;
using System.Collections;
using FarmingPlants;
using InteractionSystem;
using InventorySystem.Model;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/ItemSystem/Items/ToolItem", fileName = "ToolItemData")]
public class ToolItemData : ItemData, IUsable
{
    [SerializeField] private ToolType _toolType;
    [SerializeField] private ToolLevel _toolLevel;

    public ToolType Type => _toolType;
    public ToolLevel Level => _toolLevel;

    public void RightBtnUse(Interactor currentInteractor, InventorySlot slot, System.Action onUseComplete){}

    public void LeftBtnUse(Interactor currentInteractor, InventorySlot slot, Action onUseComplete)
    {
        if(IsCanUse(currentInteractor, slot))
        {
            StartUseTool(currentInteractor, slot, onUseComplete);
        }
    }

    private bool IsCanUse(Interactor currentInteractor, InventorySlot slot)
    {
        Land land = currentInteractor.CurrentInteraction as Land;
        switch(Type)
        {
            case ToolType.Hoe:
                return land != null && !land.IsPlowed;
            case ToolType.WateringCan:
                return land != null && !land.IsWatered;
            default:
                return false;
        }
    }

    private void StartUseTool(Interactor currentInteractor, InventorySlot slot, Action onUseComplete)
    {
        switch (Type)
        {
            case ToolType.Axe:
                // Axe action
                break;
            case ToolType.PickAxe:
                // PickAxe action
                break;
            case ToolType.Hoe:
                PlayAnimationAndUseTool(currentInteractor, slot, onUseComplete);
                break;
            case ToolType.WateringCan:
                PlayAnimationAndUseTool(currentInteractor, slot, onUseComplete);
                break;
            case ToolType.Sword:
                // Sword action
                break;
        }
    }

    private void PlayAnimationAndUseTool(Interactor currentInteractor, InventorySlot slot, Action onUseComplete)
    {
        PlayerController playerController = currentInteractor.GetComponent<PlayerController>();
        EntityAnimator playerAnimator = playerController.PlayerAnimator;
        string animationName = GetAnimationName();
        IEnumerator actionRoutine = UseToolRoutine(animationName, currentInteractor, slot, onUseComplete);
        playerController.PerformAction(actionRoutine);
    }

    private IEnumerator UseToolRoutine(string animationName, Interactor currentInteractor, InventorySlot slot, Action onUseComplete)
    {
        EntityAnimator playerAnimator = currentInteractor.GetComponent<EntityAnimator>();
        yield return playerAnimator.PlayAnimationRoutine(animationName, () => {
            UseTool(currentInteractor, slot, onUseComplete);
        });
    }

    private void UseTool(Interactor currentInteractor, InventorySlot slot, Action onUseComplete)
    {
        Land land = currentInteractor.CurrentInteraction as Land;
        switch (Type)
        {
            case ToolType.Axe:
                // Axe action
                break;
            case ToolType.PickAxe:
                // PickAxe action
                break;
            case ToolType.Hoe:
                land?.PlowTheLand();
                break;
            case ToolType.WateringCan:
                land?.WaterTheLand();
                break;
            case ToolType.Sword:
                // Sword action
                break;
        }
        onUseComplete?.Invoke();
    }

    public string GetAnimationName()
    {
        switch (Type)
        {
            case ToolType.Hoe:
                return "Digging";
            case ToolType.WateringCan:
                return "Watering";
            default:
                return "PickUp";
        }
    }

    public enum ToolType
    {
        Axe,
        PickAxe,
        Hoe,
        WateringCan,
        Sword,
    }

    public enum ToolLevel
    {
        Level1,
        Level2,
        Level3
    }
}