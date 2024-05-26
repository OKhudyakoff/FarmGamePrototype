using InteractionSystem;
using InventorySystem.Model;

public interface IUsable
{
    void LeftBtnUse(Interactor currentInteractor, InventorySlot slot, System.Action onUseComplete);
    void RightBtnUse(Interactor currentInteractor, InventorySlot slot, System.Action onUseComplete);
    string GetAnimationName();
}
