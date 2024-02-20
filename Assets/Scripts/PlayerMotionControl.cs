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
            index: 0,
            transform: gameObject.transform,
            displacementMultiplier: 5,
            tweenTimeInterval: 2
            ).AddTo(this);
    }

    private IDisposable ManageTouchInput(Func<int, Touch> GetTouch, int index, Func<int> TouchCount, Transform transform, float displacementMultiplier, float tweenTimeInterval)
    {
        return Observable
            .EveryValueChanged<Func<int, Touch>, Func<Touch>>(GetTouch, Entry => () => Entry(index))
            .ThrottleLast(TimeSpan.FromMilliseconds(125))
            .Where(Entry => TouchCount() > 0)
            .Select<Func<Touch>, Action>(Entry =>
            {
                return Entry().phase switch
                {
                    TouchPhase.Moved => () =>
                    {
                        transform.DOMoveX(transform.position.x + displacementMultiplier * Mathf.Sign(Entry().deltaPosition.x), tweenTimeInterval);
                    }
                    ,
                    _ => null,
                };
            })
            .Where(action => action != null)
            .Subscribe(action => action());
    }
}
