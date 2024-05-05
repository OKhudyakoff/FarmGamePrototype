using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(FPS_Counter))]
public class FPS_Counter_Dispay : MonoBehaviour
{
    private FPS_Counter counter;
    [SerializeField] private TMP_Text fpsTMP;

    private void Start()
    {
        counter = GetComponent<FPS_Counter>();
    }

    private void Update()
    {
        if(fpsTMP != null)
        {
            fpsTMP.text = "FPS: " + counter.FPS.ToString();
        }
    }
}
