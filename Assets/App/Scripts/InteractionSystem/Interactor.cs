using FarmingPlants;
using UnityEngine;

namespace InteractionSystem
{
    public abstract class Interactor : MonoBehaviour
    {
        protected IInteraction currentInteraction;
        protected float _interactionRadius = 0.5f;
        public IInteraction CurrentInteraction => currentInteraction;

        public void SetInteraction(IInteraction interactionTarget)
        {
            currentInteraction = interactionTarget;
        }

        public abstract void Interact();
    }
}