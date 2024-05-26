using UnityEngine;

public class FpsCounter : MonoBehaviour
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
