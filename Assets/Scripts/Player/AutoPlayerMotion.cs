using DG.Tweening;
using R3;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoPlayerMotion : MonoBehaviour
{
    [SerializeField] private SO_PlayerMotionSettings _settings;

    private void OnEnable()
    {
        GetComponent<Rigidbody>().velocity = _settings.ForwardSpeed() * Vector3.forward;
    }
}
