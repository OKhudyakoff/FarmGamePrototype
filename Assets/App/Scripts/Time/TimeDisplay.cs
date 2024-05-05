using UnityEngine;
using TMPro;
using Utilities;

public class TimeDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text displayTime;
    [SerializeField] private TMP_Text displaySeason;
    [SerializeField] private TMP_Text displayDay;

    private void OnEnable()
    {
        TimeManager.OnDateTimeChanged += UpdateDisplay;
    }

    private void OnDisable()
    {
        TimeManager.OnDateTimeChanged -= UpdateDisplay;
    }

    private void UpdateDisplay(DateTime dateTime)
    {
        displayTime.text = dateTime.TimeToString();
        displaySeason.text = dateTime.Season.ToString();
        displayDay.text = dateTime.Day.ToString();
    }
}
