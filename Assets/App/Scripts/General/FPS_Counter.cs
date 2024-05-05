using UnityEngine;

public class FPS_Counter : MonoBehaviour
{
    public int FPS { get; private set; }

    private void Start()
    {
        Application.targetFrameRate = 120;
    }
    void Update()
    {
        FPS = (int)(1f/Time.deltaTime);
    }
}
