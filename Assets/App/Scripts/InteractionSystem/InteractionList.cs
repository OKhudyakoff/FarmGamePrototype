using InteractionSystem;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Outline))]
public class InteractionList : MonoBehaviour, IInteraction
{
    [SerializeField] private GameObject _interactionPanel;
    [SerializeField] private Transform _buttonHolderl;
    [SerializeField] private InterationUIButton _interactionButtonPrefab;
    [SerializeField] private List<ActionObject> _actions = new List<ActionObject>();
    private List<InterationUIButton> _buttonList = new List<InterationUIButton>();
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
                InterationUIButton newButton = Instantiate(_interactionButtonPrefab, _buttonHolderl);
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
        _interactionPanel.SetActive(false);
        Mouse.LockCursor();
    }

    public void Interact(Interactor interactor)
    {
        if(!_isInteractionStarted)
        {
            _isInteractionStarted = true;
            _interactionPanel.SetActive(true);
            Mouse.UnlockCursor();
        }
        else
        {
            _isInteractionStarted = false;
            _interactionPanel.SetActive(false);
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
