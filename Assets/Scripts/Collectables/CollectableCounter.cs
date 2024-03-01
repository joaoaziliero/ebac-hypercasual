using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using R3;
using R3.Triggers;
using System;
using TMPro;

public class CollectableCounter : MonoBehaviour
{
    protected IDisposable ManageCollections(Collider playerCollider, string tagForCollectable, TextMeshProUGUI textDisplay)
    {
        return playerCollider
            .OnTriggerEnterAsObservable()
            .Where(collision => collision.gameObject.CompareTag(tagForCollectable))
            .Scan(0, (accumulator, collision) => accumulator + 1)
            .Subscribe(accumulator => UpdateCount(textDisplay, accumulator));
    }

    private readonly Action<TextMeshProUGUI, int> UpdateCount =
        (textDisplay, accumulator) => { textDisplay.text = accumulator.ToString(); };
}
