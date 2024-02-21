using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using R3;
using System;
using DG.Tweening;

public class PlayerMotionControl : MonoBehaviour
{
    private void Start()
    {
        ManageTouchInput(
            TouchCount: () => Input.touchCount,
            GetTouch: Input.GetTouch,
            fingerIndex: 0,
            transform: gameObject.transform,
            displacementMultiplier: 3
            ).AddTo(this);
    }

    private IDisposable ManageTouchInput(Func<int, Touch> GetTouch, int fingerIndex, Func<int> TouchCount, Transform transform, float displacementMultiplier)
    {
        return Observable
            .EveryValueChanged<Func<int, Touch>, Func<Touch>>(GetTouch, Entry => () => Entry(fingerIndex))
            .Where(Entry => TouchCount() > 0)
            .Select<Func<Touch>, Action>(Entry =>
            {
                return Entry().phase switch
                {
                    TouchPhase.Moved => () =>
                    {
                        transform.DOMoveX(transform.position.x + displacementMultiplier * Mathf.Sign(Entry().deltaPosition.x), 1);
                    }
                    ,
                    TouchPhase.Ended => () =>
                    {
                        var screenPos = Camera.main.WorldToScreenPoint(transform.position);
                        transform.DOMove(Camera.main.ScreenToWorldPoint(new Vector3(Entry().position.x, screenPos.y, screenPos.z)), 1);
                    }
                    ,
                    _ => null
                };
            })
            .Where(action => action != null)
            .Subscribe(action => action());
    }
}
