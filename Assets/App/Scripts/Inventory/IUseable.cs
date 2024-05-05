using InteractionSystem;
using InventorySystem.Model;

public interface IUseable
{
    public void Use(Interactor currentInteractor, InventorySlot slot);
}
