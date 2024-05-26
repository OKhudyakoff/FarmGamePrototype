using UnityEngine;
using UnityEngine.Windows;

public class CameraRotation : MonoBehaviour
{
    [SerializeField] private Transform _cameraFollowTarget;
    [SerializeField] private float _rotationPower;
    private InputHandler _inputHandler;
    private Mouse _mouse;
    private const float _threshold = 0.01f;
    private float _cinemachineTargetYaw;
    private float _cinemachineTargetPitch;
    private float CameraAngleOverride = 0.0f;
    private float TopClamp = 70.0f;
    private float BottomClamp = -30.0f;

    private void Start()
    {
        _inputHandler = ServiceLocator.Current.Get<InputHandler>();
        _mouse = ServiceLocator.Current.Get<Mouse>();
    }
    private void LateUpdate()
    {
        /*if(_mouse.IsCursorLocked)
        {
            //transform.rotation *= Quaternion.AngleAxis(_inputHandler.MouseDelta.x * _rotationPower, Vector3.up);

            _cameraFollowTarget.rotation *= Quaternion.AngleAxis(-_inputHandler.MouseDelta.y * _rotationPower, Vector3.right);
            var angles = _cameraFollowTarget.localEulerAngles;
            angles.z = 0;

            var angle = _cameraFollowTarget.localEulerAngles.x;
            if (angle > 180 && angle < 300)
            {
                angles.x = 300;
            }
            else if (angle < 180 && angle > 70)
            {
                angles.x = 70;
            }
            _cameraFollowTarget.localEulerAngles = angles;

            transform.rotation = Quaternion.Euler(0, _cameraFollowTarget.rotation.eulerAngles.y, 0);
            _cameraFollowTarget.localEulerAngles = new Vector3(angles.x, 0, 0);
        }*/

        SetRotation();
    }

    private void SetRotation()
    {
        if (_inputHandler.MouseDelta.sqrMagnitude >= _threshold && _mouse.IsCursorLocked)
        {
            _cinemachineTargetYaw += _inputHandler.MouseDelta.x * _rotationPower;
            _cinemachineTargetPitch -= _inputHandler.MouseDelta.y * _rotationPower;
        }
        _cinemachineTargetYaw = ClampAngle(_cinemachineTargetYaw, float.MinValue, float.MaxValue);

        _cinemachineTargetPitch = ClampAngle(_cinemachineTargetPitch, BottomClamp, TopClamp);

        _cameraFollowTarget.transform.rotation = Quaternion.Euler(_cinemachineTargetPitch + CameraAngleOverride,
            _cinemachineTargetYaw, 0.0f);
    }

    private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
    {
        if (lfAngle < -360f) lfAngle += 360f;
        if (lfAngle > 360f) lfAngle -= 360f;
        return Mathf.Clamp(lfAngle, lfMin, lfMax);
    }
}
