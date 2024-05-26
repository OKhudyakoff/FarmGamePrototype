using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utilities;

public class Calendar : MonoBehaviour
{
    [SerializeField] private Button _nextSeasonBtn;
    [SerializeField] private Button _prevSeasonBtn;
    [SerializeField] private TMP_Text _seasonTMP;
    [SerializeField] private List<CalendarPanel> dayDisplays = new List<CalendarPanel>();
    [SerializeField] private List<CalendarFeastData> _feasts = new List<CalendarFeastData>();

    private DateTime currentDate;
    private Season _season;

    private void OnEnable()
    {
        TimeManager.OnDateTimeChanged += UpdateDisplay;
    }

    private void OnDisable()
    {
        TimeManager.OnDateTimeChanged -= UpdateDisplay;
    }

    private void Start()
    {
        currentDate = TimeManager.DateTime;
        _nextSeasonBtn.onClick.AddListener(() => SwitchSeason(1));
        _prevSeasonBtn.onClick.AddListener(() => SwitchSeason(-1));
        _season = currentDate.Season;
        Init();
        UpdateFeastData();
        FillThePanels();
    }

    private void Init()
    {
        for (int i = 0; i < dayDisplays.Count; i++)
        {
            dayDisplays[i].Init(this, i + 1);
        }
    }

    private void SwitchSeason(int value)
    {
        _season += value;
        if (_season < Season.Зима) _season = Season.Осень;
        else if (_season > Season.Осень) _season = Season.Зима;
        UpdateFeastData();
        FillThePanels();
    }

    private void UpdateDisplay(DateTime date)
    {
        if(currentDate.Date != date.Date)
        {
            currentDate = date;
        }
    }

    private void FillThePanels()
    {
        _seasonTMP.text = _season.ToString().ToUpper();
        for (int i = 0; i < dayDisplays.Count; i++)
        {
            if (_season == currentDate.Season && i + 1 == currentDate.Date)
            {
                dayDisplays[i].SetHilight();
            }
            else
            {
                dayDisplays[i].RemoveHilight();
            }
        }
    }

    public void UpdateFeastData()
    {
        for (int i = 0; i < dayDisplays.Count; i++)
        {
            dayDisplays[i].ClearFeastData();
            foreach (var feast in _feasts)
            {
                if ((i+1) == feast.FeastDate.Date && _season == feast.FeastDate.Season)
                {
                    dayDisplays[i].AssignFeastData(feast);
                }
            }
        }
    }

    public void OnPointerEnter(CalendarFeastData data)
    {
        ServiceLocator.Current.Get<ToolTipController>().Show(data.FeastName);
    }

    public void OnPointerExit()
    {
        ServiceLocator.Current.Get<ToolTipController>().Hide();
    }
}
