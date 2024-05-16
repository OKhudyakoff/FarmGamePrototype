using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookOnCamera : MonoBehaviour
{
    [SerializeField] private Camera _camera;

    private void Start()
    {
        _camera = Camera.main;
    }

    private void LateUpdate()
    {
        transform.rotation = Quaternion.LookRotation(transform.position - _camera.transform.position);
    }
}
