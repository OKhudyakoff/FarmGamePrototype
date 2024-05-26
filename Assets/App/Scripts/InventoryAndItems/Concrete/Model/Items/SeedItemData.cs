using System;
using FarmingPlants;
using InteractionSystem;
using InventorySystem.Model;
using UnityEngine;

namespace InventorySystem
{
    [CreateAssetMenu(menuName = "Scriptable Objects/ItemSystem/Items/SeedItem", fileName = "SeedItemData")]
    public class SeedItemData : ItemData, IUsable
    {
        [SerializeField] private PlantData _plantData;

        public PlantData PlantData => _plantData;

        public string GetAnimationName()
        {
            return "Seed";
        }

        public void LeftBtnUse(Interactor currentInteractor, InventorySlot slot, Action onUseComplete)
        {
            
        }

        public void RightBtnUse(Interactor currentInteractor, InventorySlot slot, Action onUseComplete)
        {
            if (currentInteractor == null)
            {
                return;
            }
            Land land = currentInteractor.CurrentInteraction as Land;
            if (land != null && land.IsFree())
            {
                land.Plant(_plantData);
                slot.DecreaseQuantity(1);
                GameDebugger.ShowInfo($"{ItemName} успешно посажены.");
            }
        }
    }
}
