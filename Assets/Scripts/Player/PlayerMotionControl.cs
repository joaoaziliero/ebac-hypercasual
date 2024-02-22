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
            speed: _settings.HorizontalSpeed(),
            smoothFinishTime: _settings.SmoothFinishTime()
            ).AddTo(this);
    }

    private IDisposable ManageTouchInput(Func<int> TouchCount, Func<int, Touch> GetTouch, Transform transform, Camera camera, float speed, float smoothFinishTime)
    {
        return Observable
            .EveryValueChanged<Func<int, Touch>, Func<Touch>>(GetTouch, Entry => () => Entry(0))
            .Where(Entry => TouchCount() > 0)
            .Select<Func<Touch>, Action>(Entry =>
            {
                return Entry().phase switch
                {
                    TouchPhase.Moved => () =>
                    {
                        transform.DOMoveX(speed * Mathf.Sign(Entry().deltaPosition.x), smoothFinishTime).SetRelative(true);
                    }
                    ,
                    TouchPhase.Ended => () =>
                    {
                        var screenPos = camera.WorldToScreenPoint(transform.position);
                        var worldPos = camera.ScreenToWorldPoint(new Vector3(Entry().position.x, 0, screenPos.z));
                        transform.DOMoveX(worldPos.x, smoothFinishTime);
                    }
                    ,
                    _ => null
                };
            })
            .Where(action => action != null)
            .Subscribe(action => action());
    }
}
