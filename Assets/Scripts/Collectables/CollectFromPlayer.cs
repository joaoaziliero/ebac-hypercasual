using R3;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CollectFromPlayer : CollectableCounter
{
    public TextMeshProUGUI coinsHeadsUpDisplay;
    public string tagForCoins;

    private void Start()
    {
        ManageCollections(
            playerCollider: GetComponent<Collider>(),
            tagForCollectable: tagForCoins,
            textDisplay: coinsHeadsUpDisplay)
            .AddTo(this);
    }
}
