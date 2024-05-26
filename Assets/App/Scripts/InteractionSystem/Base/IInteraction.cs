using InteractionSystem;

public interface IInteraction
{
    public void Interact(Interactor interactor);
    public void SelectObject();
    public void DeselectObject();
}
