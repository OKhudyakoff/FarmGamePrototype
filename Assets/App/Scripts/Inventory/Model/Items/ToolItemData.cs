using FarmingPlants;
using InteractionSystem;
using InventorySystem.Model;
using UnityEngine;

[CreateAssetMenu(menuName = "Inventory/ToolItem", fileName = "ToolItemData")]
public class ToolItemData : ItemData, IUseable
{
    [SerializeField] private ToolType _toolType;
    [SerializeField] private ToolLevel _toolLevel;

    public ToolType Type => _toolType;
    public ToolLevel Level => _toolLevel;

    public override ToolItemData GetTool()
    {
        return this;
    }

    public void Use(Interactor currentInteractor, InventorySlot slot)
    {
        Land land = currentInteractor.CurrentInteraction as Land;
        switch (Type)
        {
            case ToolType.Axe:
                break;
            case ToolType.PickAxe:
                break;
            case ToolType.hoe:
                if(land != null)
                {
                    land.PlowTheLand();
                }
                break;
            case ToolType.WateringCan:
                if (land != null)
                {
                    land.WaterTheLand();
                }
                break;
            case ToolType.Sword:
                break;
        }
    }

    public enum ToolType
    {
        Axe,
        PickAxe,
        hoe,
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
