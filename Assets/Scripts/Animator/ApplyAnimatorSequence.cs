using R3;
using R3.Triggers;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyAnimatorSequence : MonoBehaviour
{
    [SerializeField] private string _firstAnimationTrigger;
    [SerializeField] private string _lastAnimationTrigger;

    private void OnEnable()
    {
        PlayAnimation(transform.parent.GetComponentInChildren<Animator>(), _firstAnimationTrigger);
    }

    private void OnDestroy()
    {
        PlayAnimation(transform.parent.GetComponentInChildren<Animator>(), _lastAnimationTrigger);
    }

    private readonly Action<Animator, string> PlayAnimation = (animator, name) => { animator.SetTrigger(name); };
}
