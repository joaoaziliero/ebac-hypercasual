using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using R3;
using System;
using DG.Tweening;

public class PlayerMotionControl : MonoBehaviour
{
    [SerializeField] private SO_PlayerMotionSettings _settings;

    private void Start()
    {
        ManageTouchInput(
            TouchCount: () => Input.touchCount,
            GetTouch: Input.GetTouch,
            transform: gameObject.transform,
            camera: Camera.main,
            clamp: _settings.clampX,
            speed: _settings.HorizontalSpeed(),
            smoothFinishTime: _settings.SmoothFinishTime()
            ).AddTo(this);
    }

    private IDisposable ManageTouchInput(Func<int> TouchCount, Func<int, Touch> GetTouch, Transform transform, Camera camera, float speed, float clamp, float smoothFinishTime)
    {
        return Observable
            .EveryValueChanged<Func<int, Touch>, Func<Touch>>(GetTouch, Entry => () => Entry(0))
            .Where(Entry => TouchCount() > 0)
            .Select(Entry =>
            {
                return Entry().phase switch
                {
                    TouchPhase.Moved => speed * Mathf.Sign(Entry().deltaPosition.x),
                    TouchPhase.Ended => Mathf.Clamp(camera.ScreenToWorldPoint(
                        new Vector3(Entry().position.x, 0, camera.WorldToScreenPoint(transform.position).z)).x, -clamp, clamp) -
                        transform.position.x,
                    _ => 0,
                };
            })
            .Subscribe(deltaX => MoveXByTween(transform, deltaX, smoothFinishTime));
    }

    private readonly Action<Transform, float, float> MoveXByTween =
        (transform, distance, time) => transform.DOMoveX(distance, time).SetRelative(true);
}
