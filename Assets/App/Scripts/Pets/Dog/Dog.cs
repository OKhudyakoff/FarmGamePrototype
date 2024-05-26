using UnityEngine;
using UnityEngine.AI;
using static Unity.VisualScripting.Member;

public class Dog : MonoBehaviour
{
    private Animator _animator;
    private NavMeshAgent _agent;
    private Transform _target;

    private const string sitKey = "Sit_b";
    private const string sleepKey = "Sleep_b";

    private bool _isFollowing = false;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _agent = GetComponent<NavMeshAgent>();
        SetTarget(ServiceLocator.Current.Get<PlayerController>().transform);
        _isFollowing = false;
    }

    private void SetTarget(Transform target)
    {
        _target = target;
    }

    private void Update()
    {
        if(_isFollowing)
        {
            _agent.SetDestination(_target.position);
            _animator.SetFloat("Movement_f", _agent.velocity.magnitude);
        }
        ApplyRotationAnim();
    }

    private void ApplyRotationAnim()
    {
        var turnAngle = Vector3.SignedAngle(transform.forward, _agent.velocity, Vector3.up);

        _animator.SetInteger("TurnAngle_int", (int)turnAngle);
    }

    public void Sit()
    {
        ResetAnim();
        _animator.SetBool(sitKey, true);
    }

    public void Sleep()
    {
        ResetAnim();
        _animator.SetBool(sleepKey, true);
    }

    public void Follow()
    {
        ResetAnim();
        _isFollowing = true;
    }

    public void ResetAnim()
    {
        _isFollowing = false;
        _animator.SetBool(sitKey, false);
        _animator.SetBool(sleepKey, false);
    }
}
