using InteractionSystem;
using InventorySystem;
using System;
using UnityEngine;

[RequireComponent(typeof(Outline))]
public class CraftTable : MonoBehaviour, IInteraction
{
    [SerializeField] private BaseWindow _craftCanvas;
    [SerializeField] private bool isOpened = false;
    private CraftSystem _craftSystem;
    private Outline _outline;

    private void Start()
    {
        _outline = GetComponent<Outline>();
        _craftSystem = GetComponent<CraftSystem>();

        DeselectObject();
        isOpened = false;
    }

    public void Interact(Interactor interactor)
    {
        isOpened = _craftCanvas.gameObject.activeSelf;
        isOpened = !isOpened;
        UpdateState();
    }

    private void UpdateState()
    {
        if (isOpened)
        {
            _craftCanvas.Open();
        }
        else
        {
            _craftCanvas.Close();
        }

        _craftSystem.UpdateConnectionWithPlayerInventory(isOpened);
    }

    public void SelectObject()
    {
        _outline.enabled = true;
    }

    public void DeselectObject()
    {
        isOpened = false;
        UpdateState();
        _outline.enabled = false;
    }
}
