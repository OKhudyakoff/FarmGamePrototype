using System;
using UnityEngine;
using Utilities;

public class DayNightController : MonoBehaviour
{
    [SerializeField] private Gradient lightGradient;
    [SerializeField] private Light _directionalLight;
    [SerializeField] private AnimationCurve dayNightCurve;
    private float _tmp = 0;
    private float _seconds;
    private TimeManager _timeManager;
    private float _ticksCountInDay;

    private void OnEnable()
    {
        //TimeManager.OnDateTimeChanged += TimeChanged;
    }

    private void OnDisable()
    {
        //TimeManager.OnDateTimeChanged -= TimeChanged;
    }

    private void Start()
    {
        _timeManager = ServiceLocator.Current.Get<TimeManager>();
        _ticksCountInDay = _timeManager.TicksCountInDay();
        Debug.Log(_ticksCountInDay);
        _tmp = ((float)TimeManager.DateTime.TotalMinutes % 1440 / 1440) * _ticksCountInDay;
    }

    private void Update()
    {
        if(!_timeManager.IsPaused)
        {
            _tmp += Time.deltaTime;
            if (_tmp >= _ticksCountInDay)
            {
                _tmp = 0;
            }
            _seconds = _tmp % _ticksCountInDay / _ticksCountInDay;
            _directionalLight.color = lightGradient.Evaluate(_seconds);
            _directionalLight.intensity = Mathf.Lerp(1, 0.5f, dayNightCurve.Evaluate(_seconds));
        }
    }

    private void TimeChanged(Utilities.DateTime time)
    {

    }
}
