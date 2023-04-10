using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationSwitcher : MonoBehaviour
{
    public Animator anim;
    private string currentState;
    [HideInInspector]
    public delegate void OnComplete();

    public void ChangeAnimationState(string newState) {
        ChangeAnimationState(newState, false, null);
    }

    public void ChangeAnimationState(string newState, bool restart) {
        ChangeAnimationState(newState, restart, null);
    }

    public void ChangeAnimationState(string newState, bool restart, OnComplete onComplete) {
        if (currentState == newState) return;

        anim.Play(newState);

        currentState = newState;

        if (onComplete == null) return;

        StartCoroutine(WaitForComplete(onComplete));
    }

    IEnumerator WaitForComplete(OnComplete onComplete) {
        yield return new WaitForSeconds((anim.runtimeAnimatorController.animationClips[0].length));

        onComplete();
    }
}
