using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace WitchOS
{
public class BootCycleHandler : MonoBehaviour
{
    public Animator Animator;
    public Image Image;
    public string AnimatorOnStateBool, AnimatorTrigger, IdleStateName, ShutDownStateName;

    int idleAnimationNameHash, shutDownAnimationNameHash;

    void Start ()
    {
        idleAnimationNameHash = Animator.StringToHash(IdleStateName);
        shutDownAnimationNameHash = Animator.StringToHash(ShutDownStateName);
    }

    public void ShutDown ()
    {
        StartCoroutine(shutDownRoutine());
    }

    public void StartUp ()
    {
        StartCoroutine(startUpRoutine());
    }

    IEnumerator shutDownRoutine ()
    {
        Image.raycastTarget = true;

        Animator.SetBool(AnimatorOnStateBool, false);
        Animator.SetTrigger(AnimatorTrigger);

        yield return new WaitUntil(() => Animator.GetCurrentAnimatorStateInfo(0).shortNameHash == shutDownAnimationNameHash);
        yield return new WaitUntil(() => Animator.GetCurrentAnimatorStateInfo(0).shortNameHash == idleAnimationNameHash);

        TimeState.Instance.StartNewDay();
    }

    IEnumerator startUpRoutine ()
    {
        Animator.SetBool(AnimatorOnStateBool, true);
        Animator.SetTrigger(AnimatorTrigger);

        yield return new WaitUntil(() => Animator.GetCurrentAnimatorStateInfo(0).shortNameHash == idleAnimationNameHash);

        Image.raycastTarget = false;
    }
}
}
