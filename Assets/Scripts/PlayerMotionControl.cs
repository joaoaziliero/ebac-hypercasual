using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using R3;
using System;

public class PlayerMotionControl : MonoBehaviour
{
    private void Start()
    {
        ManageTouchInput(Input.GetTouch, 0, () => Input.touchCount).AddTo(this);
    }

    private IDisposable ManageTouchInput(Func<int, Touch> GetTouch, int index, Func<int> TouchCount)
    {
        return Observable
            .EveryValueChanged<Func<int, Touch>, Func<Touch>>(GetTouch, Entry => () => Entry(index))
            .Where(Entry => TouchCount() > 0)
            .Scan(new Vector2[] { Vector2.zero, Vector2.zero }, (array, Entry) =>
            {
                return Entry().phase switch
                {
                    TouchPhase.Began => new Vector2[] { Entry().position, Vector2.zero },
                    TouchPhase.Moved => new Vector2[] { array[0], Entry().position - array[0] },
                    TouchPhase.Stationary => new Vector2[] { Entry().position, Vector2.zero },
                    _ => new Vector2[] { Vector2.zero, Vector2.zero }
                };
            })
            .Select(array => array[1].x)
            .Subscribe(deltaX => { transform.position += 0.01f * new Vector3(deltaX, 0, 0); } );
    }
}
