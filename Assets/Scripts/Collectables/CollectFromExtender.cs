using R3;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectFromExtender : CollectableCounter
{
    private void Start()
    {
        ManageCollections(
            playerCollider: GetComponent<Collider>(),
            tagForCollectable: GetComponentInParent<CollectFromPlayer>().tagForCoins,
            textDisplay: GetComponentInParent<CollectFromPlayer>().coinsHeadsUpDisplay)
            .AddTo(this);
    }
}
