using InteractionSystem;
using UnityEngine;

[RequireComponent(typeof(Outline))]
public class DoorInteraction : MonoBehaviour, IInteraction
{
    private Animator _animator;
    private Outline _outline;
    private const string _animKey = "IsClosed";
    private bool _isClosed = true;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _outline = GetComponent<Outline>();
        DeselectObject();
        _isClosed = true;
        _animator.SetBool(_animKey, _isClosed);
    }

    public void DeselectObject()
    {
        _outline.enabled = false;
    }

    public void Interact(Interactor interactor)
    {
        _isClosed = !_isClosed;
        _animator.SetBool(_animKey,_isClosed);
    }

    public void SelectObject()
    {
        _outline.enabled = true;
    }
}
