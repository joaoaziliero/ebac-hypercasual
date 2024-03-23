using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[CreateAssetMenu]
public class SO_Tweens : ScriptableObject
{
    public List<TweenSettings> transformTween;
}

[System.Serializable]
public class TweenSettings
{
    public Vector3 target;
    public float duration;
    public Ease ease;
    public float delay;
}
