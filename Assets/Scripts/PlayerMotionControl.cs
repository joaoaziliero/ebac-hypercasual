using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using R3;
using System;

public class PlayerMotionControl : MonoBehaviour
{
    private void Start()
    {
        ManageTouchInput(Input.GetTouch, 0).AddTo(this);
    }

    private IDisposable ManageTouchInput(Func<int, Touch> GetTouch, int index)
    {
        return Observable
            .EveryValueChanged<Func<int, Touch>, Func<Touch>>(GetTouch, Entry => () => Entry(index))
            .Scan(new Vector2[] { Vector2.zero, Vector2.zero }, (acc, Entry) =>
            {
                try
                {
                    return Entry().phase switch
                    {
                        TouchPhase.Began => new Vector2[] { Entry().position, Vector2.zero },
                        TouchPhase.Moved => new Vector2[] { acc[0], Entry().position - acc[0] },
                        TouchPhase.Stationary => acc,
                        _ => new Vector2[] { Vector2.zero, Vector2.zero }
                    };
                }
                catch (ArgumentException)
                {
                    return new Vector2[] { Vector2.zero, Vector2.zero };
                }
            })
            .Subscribe(list => Debug.Log(list[1]));
    }
}
