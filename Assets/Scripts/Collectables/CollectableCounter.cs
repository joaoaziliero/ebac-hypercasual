using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using R3;
using R3.Triggers;
using System;
using TMPro;

public class CollectableCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _coinsHeadsUpDisplay;
    [SerializeField] private string _tagForCoins;

    private void Start()
    {
        ManageCollections(
            playerCollider: GetComponent<Collider>(),
            tag: _tagForCoins,
            HUD: _coinsHeadsUpDisplay)
            .AddTo(this);
    }

    private IDisposable ManageCollections(Collider playerCollider, string tag, TextMeshProUGUI HUD)
    {
        return playerCollider
            .OnTriggerEnterAsObservable()
            .Where(collision => collision.gameObject.CompareTag(tag))
            .Select<Collider, Action>(collision => () =>
            {
                collision.gameObject.SetActive(false);
                HUD.text = (int.Parse(HUD.text) + 1).ToString();
            })
            .Subscribe(action => action());
    }
}
