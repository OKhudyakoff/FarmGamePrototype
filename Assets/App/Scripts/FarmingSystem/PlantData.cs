using InventorySystem.Model;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

namespace FarmingPlants
{
    [CreateAssetMenu(menuName = "Plans/PlantData", fileName = "PlantData")]
    public class PlantData : ScriptableObject
    {
        [SerializeField] private List<GameObject> growingStagePrefabs = new List<GameObject>();

        public List<GameObject> GrowingStagePrefabs => growingStagePrefabs;

        [Header("Growth Duration")]
        [SerializeField] private DateTime growthDuration;

        public DateTime GrowthDuration => growthDuration;

        [Space,Header("Alive Data")]
        [SerializeField] private GameObject alivePlantPrefab;
        [SerializeField] private DateTime aliveTime;

        public GameObject AlivePlantPrefab => alivePlantPrefab;
        public DateTime AliveTime => aliveTime;

        [Space,Header("PickUp Data")]
        [SerializeField] private ItemData outcome;
        [SerializeField] private int count;

        public ItemData Outcome => outcome;
        public int Count => count;
    }
}
