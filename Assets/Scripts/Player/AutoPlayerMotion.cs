using DG.Tweening;
using R3;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoPlayerMotion : MonoBehaviour
{
    private void Start()
    {
        GetComponent<Rigidbody>().velocity = 5 * Vector3.forward;
    }
}
