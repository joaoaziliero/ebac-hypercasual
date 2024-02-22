using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SO_PlayerMotionSettings : ScriptableObject
{
    [SerializeField] private float forwardSpeed;
    [SerializeField] private float horizontalSpeed;

    #region DATA_RETRIEVER_FUNCTIONS
    public Func<float> ForwardSpeed { get; }
    public Func<float> HorizontalSpeed { get; }
    #endregion
    #region CLASS_CONSTRUCTOR
    private SO_PlayerMotionSettings()
    {
        ForwardSpeed = () => forwardSpeed;
        HorizontalSpeed = () => horizontalSpeed;
    }
    #endregion
}
