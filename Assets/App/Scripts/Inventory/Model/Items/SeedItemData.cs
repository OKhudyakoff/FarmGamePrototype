using FarmingPlants;
using InteractionSystem;
using InventorySystem.Model;
using UnityEngine;

namespace InventorySystem
{
    [CreateAssetMenu(menuName = "Inventory/SeedItem", fileName = "SeedItemData")]
    public class SeedItemData : ItemData, IUseable
    {
        [SerializeField] private PlantData _plantData;

        public PlantData PlantData => _plantData;

        public override SeedItemData GetSeed()
        {
            return this;
        }

        public void Use(Interactor currentInteractor, InventorySlot slot)
        {
            if (currentInteractor == null) return;
            Land land = currentInteractor.CurrentInteraction as Land;
            if (land != null && land.IsFree())
            {
                land.Plant(_plantData);
                slot.RemoveItem(1);
            }
        }
    }
}
