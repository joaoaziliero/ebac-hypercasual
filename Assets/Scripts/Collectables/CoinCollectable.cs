using R3;
using R3.Triggers;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCollectable : MonoBehaviour
{
    [SerializeField] private string _tagForPlayer;

    private void Start()
    {
        ManagePlayerInteractions(
            thisCollider: GetComponent<Collider>(),
            tagForPlayer: _tagForPlayer)
            .AddTo(this);
    }

    private IDisposable ManagePlayerInteractions(Collider thisCollider, string tagForPlayer)
    {
        return thisCollider
            .OnTriggerEnterAsObservable()
            .Where(collision => collision.gameObject.CompareTag(tagForPlayer))
            .Subscribe(_ => Destroy(this.gameObject));
    }
}
