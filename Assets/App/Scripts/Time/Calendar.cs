using System.Collections.Generic;
using UnityEngine;
using Utilities;

public class Calendar : MonoBehaviour
{
    [SerializeField] private List<CalendarPanel> dayDisplays = new List<CalendarPanel>();
    private DateTime currentDate;

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
        FillThePanels();
    }

    private void UpdateDisplay(DateTime date)
    {
        if(currentDate.Date != date.Date)
        {
            var index = (currentDate.Date - 1) < 0 ? 0 : (currentDate.Date - 1);
            dayDisplays[index].RemoveHilight();
        }
        currentDate = date;
        dayDisplays[currentDate.Date - 1].SetHilight();
    }

    private void FillThePanels()
    {
        for (int i = 0; i < dayDisplays.Count; i++)
        {
            dayDisplays[i].Init(i + 1);
            if(i + 1 == TimeManager.DateTime.Date)
            {
                dayDisplays[i].SetHilight();
            }
            else
            {
                dayDisplays[i].RemoveHilight();
            }
        }
    }
}
