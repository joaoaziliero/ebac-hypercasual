using R3;
using R3.Triggers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ApplyAnimatorSequence : MonoBehaviour
{
    [SerializeField] private List<string> AnimationTriggerNames;
    [SerializeField] private List<float> AnimationTriggerIntervals;

    private void OnEnable()
    {
        CreateAnimatorSequence(
            names: AnimationTriggerNames,
            intervals: AnimationTriggerIntervals,
            animator: transform.parent.GetComponentInChildren<Animator>())
            .ForEach(observable => observable.Subscribe());

        Destroy(this.gameObject);
    }

    private List<Observable<Unit>> CreateAnimatorSequence(List<string> names, List<float> intervals, Animator animator)
    {
        return names
            .Select((name, i) => Observable.Timer(TimeSpan.FromSeconds(intervals[i])).Do(onCompleted: _ => animator.SetTrigger(name)))
            .ToList();
    }
}
