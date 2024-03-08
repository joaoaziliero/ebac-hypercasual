using R3;
using R3.Triggers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;

public class AnimateHeightChange : MonoBehaviour
{
    [SerializeField] private string _tagForPlayer;
    [SerializeField] private float _heightDifference;
    [SerializeField] private float _totalTweeningTime;
    [SerializeField] private Ease _animationEase;
    [SerializeField] private GameObject _animatorSequenceCarrier;

    private void Start()
    {
        ManageAnimation(
            powerUpTrigger: GetComponent<Collider>(),
            tagForPlayer: _tagForPlayer,
            deltaY: _heightDifference,
            timeToReset: _totalTweeningTime,
            animationEase: _animationEase,
            carrier: _animatorSequenceCarrier)
            .AddTo(this);
    }

    private IDisposable ManageAnimation(Collider powerUpTrigger, string tagForPlayer, float deltaY, float timeToReset, Ease animationEase, GameObject carrier)
    {
        return powerUpTrigger
            .OnTriggerEnterAsObservable()
            .Where(collider => collider.gameObject.CompareTag(tagForPlayer))
            .Subscribe(collider =>
            {
                ActivateHeightTweens(collider.transform, deltaY, timeToReset, animationEase);
                SpawnAndScheduleDestruction(carrier, collider.transform, timeToReset);
                Destroy(this.gameObject);
            });
    }

    private readonly Action<Transform, float, float, Ease> ActivateHeightTweens =
        (transform, deltaY, timeSpan, ease) =>
        {
            DOTween.Sequence()
                .Append(transform.DOMoveY(Mathf.Abs(deltaY), timeSpan / 3)
                    .SetEase(ease)
                    .SetRelative(true))
                .Append(transform.DOMoveY(-Mathf.Abs(deltaY), timeSpan / 3)
                    .SetEase(ease)
                    .SetRelative(true)
                    .SetDelay(timeSpan / 3));
        };

    private readonly Action<GameObject, Transform, float> SpawnAndScheduleDestruction =
        (prefab, parent, timeSpan) => { Destroy(Instantiate(prefab, parent.transform), timeSpan); };
}
