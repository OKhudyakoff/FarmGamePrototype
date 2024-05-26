using TMPro;
using UnityEngine;

[RequireComponent(typeof(FpsCounter))]
public class FpsCounterDispay : MonoBehaviour
{
    private FpsCounter counter;
    [SerializeField] private TMP_Text fpsTMP;

    private void Start()
    {
        counter = GetComponent<FpsCounter>();
    }

    private void Update()
    {
        if(fpsTMP != null)
        {
            fpsTMP.text = "FPS: " + counter.FPS.ToString();
        }
    }
}
