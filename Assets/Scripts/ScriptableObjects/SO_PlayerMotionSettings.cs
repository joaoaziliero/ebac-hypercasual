using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SO_PlayerMotionSettings : ScriptableObject
{
    [Header("Forward Motion")]
    [SerializeField] private float forwardSpeed;
    [Header("Horizontal Motion")]
    [SerializeField] private float horizontalSpeed;
    [SerializeField] private float smoothFinishTime;

    #region DATA_RETRIEVER_FUNCTIONS
    public Func<float> ForwardSpeed { get; }
    public Func<float> HorizontalSpeed { get; }
    public Func<float> SmoothFinishTime { get; }
    #endregion
    #region CLASS_CONSTRUCTOR
    private SO_PlayerMotionSettings()
    {
        ForwardSpeed = () => forwardSpeed;
        HorizontalSpeed = () => horizontalSpeed;
        SmoothFinishTime = () => smoothFinishTime;
    }
    #endregion
}
