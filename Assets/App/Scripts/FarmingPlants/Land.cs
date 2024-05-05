using InteractionSystem;
using InventorySystem;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

namespace FarmingPlants
{
    public class Land : MonoBehaviour, IInteraction
    {
        [SerializeField] private bool testMode;

        [SerializeField] private PlantData testPlant;
        [SerializeField] private Outline outline;
        [SerializeField] private GameObject _plowModel;
        [SerializeField] private PlantedPlant _plant;

        private int _notWateredTime = 0;
        private int _wateredTime = 0;

        private bool _isWatered = false;
        private bool _isPlowed = false;

        private PlantData _plantData;
        private List<GameObject> _plantPrefabs = new List<GameObject>();
        private TimeManager _timeManager;

        public bool IsWatered => _isWatered;
        public bool IsPlowed => _isPlowed;

        private void OnEnable()
        {
            TimeManager.OnDateTimeChanged += UpdateLand;
        }

        private void OnDestroy()
        {
            TimeManager.OnDateTimeChanged -= UpdateLand;
        }

        private void Start()
        {
            _timeManager = ServiceLocator.Current.Get<TimeManager>();
            _plant.Init(this);
            DeselectObject();
            ResetLand();
        }

        public void Plant(PlantData plantData)
        {
            if(IsFree())
            {
                _plant.Plant(plantData);
            }
        }

        public void UpdateLand(DateTime date)
        {
            UpdateWaterState();
        }

        private void UpdateWaterState()
        {
            if (testMode)
            {
                _isWatered = true;
                _isPlowed = true;
                return;
            }
            if (_isWatered)
            {
                _wateredTime += _timeManager.MinutesPerTick;
                if (_wateredTime == 24 * 60)
                {
                    _isWatered = false;
                    _wateredTime = 0;
                }
            }
            else
            {
                if (_isPlowed)
                {
                    _notWateredTime += _timeManager.MinutesPerTick;
                    if (_notWateredTime == 24 * 60)
                    {
                        _isPlowed = false;
                        _notWateredTime = 0;
                        ResetLand();
                    }
                }
            }

        }

        public void WaterTheLand()
        {
            _isWatered = true;
            _wateredTime = 0;
            _notWateredTime = 0;
            Debug.Log("Watered");
        }

        public void PlowTheLand()
        {
            _isPlowed = true;
            _notWateredTime = 0;
            _plowModel.SetActive(true);
        }

        public bool IsFree()
        {
            return _isPlowed && !_plant.IsPlanted;
        }

        public void ResetLand()
        {
            if(_plant.IsPlanted) _plant.DestroyPlant();
            _isPlowed = false;
            _isWatered = false;
            _wateredTime = 0;
            _notWateredTime = 0;
            _plowModel.SetActive(false);
        }


        #region Interaction
        public void Interact(Interactor interactor)
        {
            if (_plant.IsCanPickUp() && interactor.TryGetComponent<InventoryController>(out InventoryController controller))
            {
                _plant.PickUpPlant(controller);
            }
        }

        public void SelectObject()
        {
            outline.enabled = true;
        }

        public void DeselectObject()
        {
            outline.enabled = false;
        }
        #endregion
    }
}
