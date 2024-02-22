using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using R3;
using R3.Triggers;

public class CollectableCountDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _headsUpDisplay;
    [SerializeField] private string _playerTag;

    private void Start()
    {
        ManagePlayerCollect(
            trigger: GetComponentInChildren<Collider>(),
            tag: _playerTag,
            HUD: _headsUpDisplay)
            .AddTo(this);
    }

    private IDisposable ManagePlayerCollect(Collider trigger, string tag, TextMeshProUGUI HUD)
    {
        return trigger
            .OnTriggerEnterAsObservable()
            .Where(col => col.gameObject.CompareTag(tag))
            .Select<Collider, Action>(col => () => 
            {
                HUD.text = (int.Parse(HUD.text) + 1).ToString();
                gameObject.SetActive(false);
            })
            .Subscribe(action => action());
    }
}
