using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using R3;
using R3.Triggers;
using DG.Tweening;

public class EnemyCollisions : MonoBehaviour
{
    [SerializeField] private string _tagForEnemies;
    [SerializeField] private string _deathAnimationTrigger;

    private void Start()
    {
        ManageEnemyInteractions(
            collider: GetComponent<Collider>(),
            tagForEnemies: _tagForEnemies,
            transform: transform,
            animator: GetComponentInChildren<Animator>(),
            name: _deathAnimationTrigger)
            .AddTo(this);
    }

    private IDisposable ManageEnemyInteractions(Collider collider, string tagForEnemies, Transform transform, Animator animator, string name)
    {
        return collider
            .OnCollisionEnterAsObservable()
            .Where(collision => collision.gameObject.CompareTag(tagForEnemies))
            .Subscribe(_ => { NudgeBackwards(transform); PlayAnimation(animator, name); });
    }

    private readonly Action<Transform> NudgeBackwards = (transform) => { transform.DOMoveZ(-3, 1).SetRelative(true); };
    private readonly Action<Animator, string> PlayAnimation = (animator, name) => { animator.SetTrigger(name); };
}
