using FarmingPlants;
using InventorySystem;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

public class PlantedPlant : MonoBehaviour
{
    private int _minutesNeed = 0;
    private int _currentStage = 0;
    private int _minutesPassed = 0;
    private int _totalStateCount { get { return _plantPrefabs.Count - 1; } }

    private float _growthProgress { get { return (float)_minutesPassed / _minutesNeed; } }

    private bool _isAlive;
    public bool IsPlanted => _plantData != null;

    private Land _land;
    private PlantData _plantData;
    private List<GameObject> _plantPrefabs = new List<GameObject>();
    private GameObject _alivePlant;
    private TimeManager _timeManager;

    private void OnEnable()
    {
        TimeManager.OnDateTimeChanged += UpdatePlantState;
    }

    public void Init(Land land)
    {
        _land = land;
        _isAlive = false;
        _timeManager = ServiceLocator.Current.Get<TimeManager>();
    }

    private void UpdatePlantState(DateTime dateTime)
    {
        if(_plantData != null && _land != null)
        {
            UpdatePlantProgress();
            UpdateAliveState(dateTime);
        }
    }

    private void UpdatePlantProgress()
    {
        if (_isCanGrow())
        {
            if (_growthProgress < 1)
            {
                _minutesPassed += _timeManager.MinutesPerTick;
                if (_growthProgress >= (1f / _totalStateCount) * (_currentStage + 1))
                {
                    _plantPrefabs[_currentStage].SetActive(false);
                    _currentStage++;
                    _plantPrefabs[_currentStage].SetActive(true);
                }
            }
        }
    }

    private void UpdateAliveState(DateTime date)
    {
        if (!_isAlive && _plantData.AlivePlantPrefab != null && IsAlivePlantCanSpawn(date))
        {
            _isAlive = true;
            HidePlant();
            _alivePlant = Instantiate(_plantData.AlivePlantPrefab, transform);
        }
        else if (_alivePlant != null && date.Hour == 6 && date.Minute == 0)
        {
            _isAlive = false;
            Destroy(_alivePlant);
            _alivePlant = null;
            ShowPlant();
        }
    }

    private bool _isCanGrow()
    {
        if(_land != null)
        {
            return _land.IsPlowed && _land.IsWatered;
        }
        else { return false; }
    }

    public void Plant(PlantData plantData)
    {
        _plantData = plantData;
        _minutesNeed = _plantData.GrowthDuration.DateToMinutes();
        _minutesPassed = 0;
        _currentStage = 0;
        _isAlive = false;

        foreach (var plantPrefab in _plantData.GrowingStagePrefabs)
        {
            GameObject newPlantPrefab = Instantiate(plantPrefab, transform);
            newPlantPrefab.SetActive(false);
            _plantPrefabs.Add(newPlantPrefab);
        }
        _plantPrefabs[_currentStage].gameObject.SetActive(true);
    }

    private bool IsAlivePlantCanSpawn(DateTime date)
    {
        return (date.Hour >= _plantData.AliveTime.Hour && date.Minute >= _plantData.AliveTime.Minute) && (date.Hour < 6);
    }

    private void HidePlant()
    {
        foreach (var plant in _plantPrefabs)
        {
            plant.SetActive(false);
        }
    }

    private void ShowPlant()
    {
        _plantPrefabs[_currentStage].SetActive(true);
    }

    public void DestroyPlant()
    {
        foreach (var planPrefab in _plantPrefabs)
        {
            Destroy(planPrefab);
        }
        _plantPrefabs.Clear();
        _currentStage = 0;
        _plantData = null;
    }

    public bool IsCanPickUp()
    {
        return _plantData != null && _growthProgress >= 1 && !_isAlive;
    }

    public void PickUpPlant(InventoryController controller)
    {
        if (_plantData.Outcome != null && controller != null)
        {
            controller.AddItem(_plantData.Outcome, _plantData.Count);
        }
        _land.ResetLand();
    }

}
