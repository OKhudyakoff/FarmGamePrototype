using System;
using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour, IService
{
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;
    [SerializeField] private float _speed;
    [SerializeField] private float JumpSpeed = 15f;
    [SerializeField] private float RotationSmoothTime = 0.12f;
    [SerializeField] private Camera _camera;

    private float horizontal;
    private float vertical; 
    private float gravity = 9.8f;
    private float _targetRotation = 0f;
    private float _rotationVelocity;
    private float SpeedChangeRate = 10.0f;

    private bool _isPerformingAction;

    private CharacterController _controller;
    private InputHandler _inputHandler;
    private Vector3 Player_Move;

    public EntityAnimator PlayerAnimator {get; private set;}

    public void Init()
    {
        _controller = GetComponent<CharacterController>();
        PlayerAnimator = GetComponent<EntityAnimator>();
        _inputHandler = ServiceLocator.Current.Get<InputHandler>();
        _inputHandler.OnJumpPerformed += Jump;
    }

    private void OnDestroy()
    {
        _inputHandler.OnJumpPerformed -= Jump;
    }

    void Update()
    {
        if(_isPerformingAction) return;
        Move();
        Animate();
    }

    private void Move()
    {
        float targetSpeed = _inputHandler.IsRunning ? runSpeed : walkSpeed;
        if (_inputHandler.MoveComposite == Vector2.zero) targetSpeed = 0.0f;

        float currentHorizontalSpeed = new Vector3(_controller.velocity.x, 0.0f, _controller.velocity.z).magnitude;

        float speedOffset = 0.1f;
        float inputMagnitude = 1f;

        if (currentHorizontalSpeed < targetSpeed - speedOffset ||
            currentHorizontalSpeed > targetSpeed + speedOffset)
        {
            _speed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed * inputMagnitude,
                Time.deltaTime * SpeedChangeRate);
            _speed = Mathf.Round(_speed * 1000f) / 1000f;
        }
        else
        {
            _speed = targetSpeed;
        }

        Vector3 inputDirection = new Vector3(_inputHandler.MoveComposite.x, 0.0f, _inputHandler.MoveComposite.y).normalized;
        if (_inputHandler.MoveComposite != Vector2.zero)
        {
            _targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg +
                              _camera.transform.eulerAngles.y;
            float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetRotation, ref _rotationVelocity,
                RotationSmoothTime);
            transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
        }

        vertical = _controller.isGrounded ? 0f : -gravity;


        Vector3 targetDirection = Quaternion.Euler(0.0f, _targetRotation, 0.0f) * Vector3.forward;
        _controller.Move(targetDirection.normalized * (_speed * Time.deltaTime) +
                         new Vector3(0.0f, vertical, 0.0f) * Time.deltaTime);
        /*if (_controller.isGrounded)
        {
            horizontal = _inputHandler.MoveComposite.normalized.x;
            vertical = _inputHandler.MoveComposite.normalized.y;
            Player_Move = (transform.forward * vertical + transform.right * horizontal) * _currentSpeed;
        }
        else
        {
            Player_Move.y = Player_Move.y - gravity * Time.deltaTime;
        }
        _controller.Move(Player_Move * Time.deltaTime);*/
    }

    private void Animate()
    {
        if(_inputHandler.MoveComposite.normalized.x != 0 || _inputHandler.MoveComposite.normalized.y != 0)
        {
            PlayerAnimator.FloatAnimation("move", 1);
        }
        else
        {
            PlayerAnimator.FloatAnimation("move", 0);
        }
        if(_inputHandler.IsRunning)
        {
            PlayerAnimator.SetBool("run", true);
        }
        else
        {
            PlayerAnimator.SetBool("run", false);
        }
    }

    private void Jump()
    {
        Player_Move.y = Player_Move.y + JumpSpeed;
    }

    public void PerformAction(IEnumerator actionRoutine)
    {
        if (!_isPerformingAction)
        {
            StartCoroutine(ActionRoutineWrapper(actionRoutine));
        }
    }

    private IEnumerator ActionRoutineWrapper(IEnumerator actionRoutine)
    {
        _isPerformingAction = true;
        yield return StartCoroutine(actionRoutine);
        _isPerformingAction = false;
    }
}
