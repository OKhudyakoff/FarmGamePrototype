using System;
using UnityEngine;
using UnityEngine.UI;

public class GameSettingsUI : MonoBehaviour
{
    [SerializeField] private Toggle _showDebugToogle;
    [SerializeField] private GameObject _debugPanel;

    public void Init()
    {
        _showDebugToogle.onValueChanged.AddListener(ToggleDebugPanel);
        _showDebugToogle.isOn = false;
    }

    private void ToggleDebugPanel(bool value)
    {
        _debugPanel.SetActive(value);
    }
}
