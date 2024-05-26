using InteractionSystem;
using UnityEngine;

public class PlayerInteractor : Interactor
{
    [SerializeField] private Transform _interactionPoint;
    [SerializeField] private LayerMask _interactionMask;
    [SerializeField] private int _numFound;

    private readonly Collider[] _colliders = new Collider[3];

    private void Start()
    {
        ServiceLocator.Current.Get<InputHandler>().OnInteractionTriggered += Interact;
    }

    private void OnDestroy()
    {
        ServiceLocator.Current.Get<InputHandler>().OnInteractionTriggered -= Interact;
    }

    public override void Interact()
    {
        if (currentInteraction != null)
        {
            currentInteraction.Interact(this);
        }
    }

    private void Update()
    {
        FindInteraction();
    }

    private void FindInteraction()
    {
        _numFound = Physics.OverlapSphereNonAlloc(_interactionPoint.position, _interactionRadius, _colliders, _interactionMask);
        if (_numFound > 0)
        {
            if (currentInteraction != null && _colliders[0].GetComponent<IInteraction>() != currentInteraction)
            {
                currentInteraction.DeselectObject();
            }
            currentInteraction = _colliders[0].GetComponent<IInteraction>();
            currentInteraction.SelectObject();
        }
        else
        {
            if (currentInteraction != null)
            {
                currentInteraction.DeselectObject();
                currentInteraction = null;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(_interactionPoint.position, _interactionRadius);
    }
}
