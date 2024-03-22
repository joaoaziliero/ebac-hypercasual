using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using R3;
using R3.Triggers;
using DG.Tweening;
using System;

public class PlayerTweenSet : MonoBehaviour
{
    [SerializeField] private string _tagForPowerUps;
    [SerializeField] private float _tweenDuration;
    [SerializeField] private Ease _ease;

    private void Awake()
    {
        transform.DOScale(Vector3.zero, 1).From().SetDelay(1);
    }

    private void Start()
    {
        ManagePowerUpContact
            (
            playerCollider: GetComponentInParent<Collider>(),
            tagForPowerUps: _tagForPowerUps,
            transform: GetComponent<Transform>(),
            tweenDuration: _tweenDuration,
            ease: _ease
            )
            .AddTo(this);
    }

    private IDisposable ManagePowerUpContact(Collider playerCollider, string tagForPowerUps, Transform transform, float tweenDuration, Ease ease)
    {
        return playerCollider
            .OnTriggerEnterAsObservable()
            .Where(collider => collider.gameObject.CompareTag(tagForPowerUps))
            .Subscribe(_ => { transform.DOComplete(); BounceLocalScale(transform, tweenDuration, ease); });
    }

    private readonly Action<Transform, float, Ease> BounceLocalScale = (transform, duration, ease) =>
    {
        transform.DOScale(1.2f, duration).SetLoops(2, LoopType.Yoyo).SetEase(ease);
    };
}
