using R3;
using R3.Triggers;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableEnemyCollisions : MonoBehaviour
{
    [SerializeField] private string _tagForPlayer;
    [SerializeField] private float _invincibilityTimeSpan;

    private void Start()
    {
        ManageInvincibilityTiming(
            powerUpTrigger: GetComponent<Collider>(),
            tagForPlayer: _tagForPlayer,
            _invincibilityTimeSpan)
            .AddTo(this);
    }

    private IDisposable ManageInvincibilityTiming(Collider powerUpTrigger, string tagForPlayer, float timeSpan)
    {
        return powerUpTrigger
            .OnTriggerEnterAsObservable()
            .Where(collider => collider.gameObject.CompareTag(tagForPlayer))
            .Subscribe(collider =>
            {
                EnableIsTrigger(collider);
                DisableIsTrigger(collider, timeSpan);
                Destroy(this.gameObject);
            });
    }

    private readonly Action<Collider> EnableIsTrigger = (collider) => { collider.isTrigger = true; };
    private readonly Action<Collider, float> DisableIsTrigger =
        (collider, timeSpan) => 
        {
            Observable
                .Timer(TimeSpan.FromSeconds(timeSpan))
                .Do(onCompleted: _ => { collider.isTrigger = false; })
                .Subscribe();
        };
}
