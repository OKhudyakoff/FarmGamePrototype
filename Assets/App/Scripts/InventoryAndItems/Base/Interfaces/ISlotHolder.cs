using InventorySystem.UI;

public interface ISlotHolder
{
    public void UpdateHolder();

    public void OnLeftBtnSlotClicked(InventorySlotDisplay slot);

    public void OnRightBtnSlotClicked(InventorySlotDisplay slot);
}
