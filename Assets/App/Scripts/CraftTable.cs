using InteractionSystem;
using UnityEngine;

[RequireComponent(typeof(Outline))]
public class CraftTable : MonoBehaviour, IInteraction
{
    [SerializeField] private Canvas _craftCanvas;
    private Outline _outline;
    private bool isOpened = false;

    private void Start()
    {
        _outline = GetComponent<Outline>();
        DeselectObject();
        isOpened = false;
        _craftCanvas.gameObject.SetActive(isOpened);
    }

    public void Interact(Interactor interactor)
    {
        isOpened = !isOpened;
        ShowDisplay();
    }

    private void ShowDisplay()
    {
        if(isOpened)
        {
            _craftCanvas.gameObject.SetActive(false);
            Mouse.LockCursor();
        }
        else
        {
            _craftCanvas.gameObject.SetActive(true);
            Mouse.UnlockCursor();
        }
    }

    public void SelectObject()
    {
        _outline.enabled = true;
    }

    public void DeselectObject()
    {
        _outline.enabled = false;
    }
}
