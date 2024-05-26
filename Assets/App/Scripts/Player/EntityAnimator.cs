using System.Collections;
using UnityEngine;

public class EntityAnimator : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    public void FloatAnimation(string animationName, float value)
    {
        _animator.SetFloat(animationName, value);
    }

    public void SetBool(string animationName, bool value)
    {
        _animator.SetBool(animationName, value);
    }

    public void TriggerAnimation(string animationName)
    {
        _animator.SetTrigger(animationName);
    }

    public void TriggerAnimation(string animationName, System.Action onAnimationComplete)
    {
        StartCoroutine(PlayAnimationRoutine(animationName, onAnimationComplete));
    }

    public IEnumerator PlayAnimationRoutine(string animationName, System.Action onAnimationComplete)
    {
        AnimationClip clip = FindAnimation(animationName);
        if(clip!= null)
        {
            GameDebugger.ShowInfo($"Играет анимация {animationName} для {gameObject.name}");
            _animator.Play(clip.name);
            yield return new WaitForSeconds(clip.length);
            onAnimationComplete?.Invoke();
        }
        else
        {
            GameDebugger.ShowInfo($"Не найдена анимация: {animationName} для {gameObject.name}");
        }
    }

    public AnimationClip FindAnimation (string name) 
    {
        foreach (AnimationClip clip in _animator.runtimeAnimatorController.animationClips)
        {
            if (clip.name == name)
            {
                return clip;
            }
        }
        return null;
    }
}
