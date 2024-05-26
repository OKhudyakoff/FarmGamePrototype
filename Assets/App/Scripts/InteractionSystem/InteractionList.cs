using InteractionSystem;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Outline))]
public class InteractionList : MonoBehaviour, IInteraction
{
    [SerializeField] private BaseWindow _interactionPanel;
    [SerializeField] private Transform _buttonHolderl;
    [SerializeField] private InteractionButtonUI _interactionButtonPrefab;
    [SerializeField] private List<ActionObject> _actions = new List<ActionObject>();
    private List<InteractionButtonUI> _buttonList = new List<InteractionButtonUI>();
    private Outline _outline;
    private bool _isInteractionStarted = false;

    private void Start()
    {
        _outline = GetComponent<Outline>();
        InitPanel();
        DeselectObject();
    }

    private void InitPanel()
    {
        if(_buttonList.Count == 0)
        {
            for (int i = 0; i < _actions.Count; i++)
            {
                InteractionButtonUI newButton = Instantiate(_interactionButtonPrefab, _buttonHolderl);
                int id = i;
                newButton.InteractionButton?.onClick.AddListener(() => UseAction(id));
                newButton.InteractionText.text = (i+1).ToString() + "." + _actions[i]._eventName;
                _buttonList.Add(newButton);
            }
        }
    }

    private void UseAction(int actionID)
    {
        Debug.Log(actionID);
        _actions[actionID]._event?.Invoke();
        DeselectObject();
    }

    public void DeselectObject()
    {
        _outline.enabled = false;
        _isInteractionStarted = false;
        _interactionPanel.Close();
    }

    public void Interact(Interactor interactor)
    {
        if(!_isInteractionStarted)
        {
            _isInteractionStarted = true;
            _interactionPanel.Open();
        }
        else
        {
            _isInteractionStarted = false;
            _interactionPanel.Close();
            Mouse.LockCursor();
        }
    }

    public void SelectObject()
    {
        _outline.enabled = true;
    }

    [Serializable]
    private class ActionObject
    {
        public string _eventName;
        public UnityEvent _event;
    }
}
