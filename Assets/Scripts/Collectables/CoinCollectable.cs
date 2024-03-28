using R3;
using R3.Triggers;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCollectable : MonoBehaviour
{
    [SerializeField] private string _tagForPlayer;
    [SerializeField] private GameObject _prefabTospawnOnCollection;

    private void Start()
    {
        ManagePlayerInteractions(
            thisCollider: GetComponent<Collider>(),
            tagForPlayer: _tagForPlayer,
            prefab: _prefabTospawnOnCollection)
            .AddTo(this);
    }

    private IDisposable ManagePlayerInteractions(Collider thisCollider, string tagForPlayer, GameObject prefab)
    {
        return thisCollider
            .OnTriggerEnterAsObservable()
            .Where(collision => collision.gameObject.CompareTag(tagForPlayer))
            .Subscribe(_ => { Instantiate(prefab).transform.position = this.transform.position; Destroy(this.gameObject); });
    }
}
