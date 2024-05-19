using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabSystem : MonoBehaviour
{
    [SerializeField] private List<Tab> _tabs = new List<Tab>();
    [SerializeField] Color _activeColor = Color.white;
    [SerializeField] Color _unactiveColor = Color.white;
    public int CurrentTabID { get; private set; }

    public void Init()
    {
        for (int i = 0; i < _tabs.Count; i++)
        {
            int tabId = i;
            _tabs[i]._button.onClick.AddListener(() => SwitchTab(tabId));
        }
        SwitchTab(0);
    }

    public void SwitchTab(int tabID)
    {
        CurrentTabID = tabID;
        for (int i = 0; i < _tabs.Count; i++)
        {
            if (i == CurrentTabID)
            {
                _tabs[i]._panel.SetActive(true);
                _tabs[i]._image.color = _activeColor;
            }
            else
            {
                _tabs[i]._panel.SetActive(false);
                _tabs[i]._image.color = _unactiveColor;
            }
        }
    }


    [Serializable]
    public class Tab
    {
        public GameObject _panel;
        public Button _button;
        public Image _image;
    }
}
