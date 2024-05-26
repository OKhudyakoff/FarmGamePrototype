using UnityEngine;
using Utilities;

[ExecuteAlways]
public class LightController : MonoBehaviour
{
    [SerializeField] private LightPreset _preset;
    [SerializeField] private Light _directionalLight;
    [SerializeField] private AnimationCurve _dayNightCurve;
    [SerializeField] private float _tmp;

    private float tmp = 0;
    [SerializeField, Range(0,24)] private float _seconds;
    private TimeManager _timeManager;
    private float _ticksCountInDay;

    private void OnValidate()
    {
        if (_directionalLight != null)
        {
            return;
        }

        if (RenderSettings.sun != null)
        {
            _directionalLight = RenderSettings.sun;
        }
        else
        {
            Light[] lights = FindObjectsByType<Light>(FindObjectsSortMode.None);
            foreach (Light light in lights)
            {
                if(light.type == LightType.Directional)
                {
                    _directionalLight = light;
                    return;
                }
            }
        }
    }

    private void Start()
    {
        if(Application.isPlaying)
        {
            _timeManager = ServiceLocator.Current.Get<TimeManager>();
            _ticksCountInDay = _timeManager.TicksCountInDay();
            Debug.Log(_ticksCountInDay);
            tmp = ((float)TimeManager.DateTime.TotalMinutes % 1440 / 1440) * _ticksCountInDay;
        }
    }

    private void Update()
    {
        if(Application.isPlaying)
        {
            if (!_timeManager.IsPaused)
            {
                tmp += Time.deltaTime;
                if (tmp >= _ticksCountInDay)
                {
                    tmp = 0;
                }
                _seconds = tmp % _ticksCountInDay / _ticksCountInDay;
                UpdateLighting(_seconds);
            }
        }
        else
        {
            UpdateLighting(_seconds / 24);
        }
        
    }

    private void UpdateLighting(float timePrecent)
    {
        RenderSettings.ambientLight = _preset.AmbientColor.Evaluate(timePrecent);
        RenderSettings.fogColor = _preset.FogColor.Evaluate(timePrecent);
        float tt = _dayNightCurve.Evaluate(timePrecent);
        RenderSettings.skybox.SetFloat("_Blend", tt);

        if(_directionalLight != null)
        {
            _directionalLight.color = _preset.DirectionalLightColor.Evaluate(timePrecent);
            _directionalLight.transform.localRotation = Quaternion.Euler(new Vector3((timePrecent * 360f) - _tmp, 170f, 0));
        }
    }
}
