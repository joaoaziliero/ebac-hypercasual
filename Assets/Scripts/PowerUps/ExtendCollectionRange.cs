using R3;
using R3.Triggers;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtendCollectionRange : MonoBehaviour
{
    [SerializeField] private string _tagForPlayer;
    [SerializeField] private GameObject _extenderPrefab;
    [SerializeField] private float _timeToDespawn;

    private void Start()
    {
        ManageExtension(
            powerUpTrigger: GetComponent<Collider>(),
            tagForPlayer: _tagForPlayer,
            extenderPrefab: _extenderPrefab,
            timeToDespawn: _timeToDespawn)
            .AddTo(this);
    }

    private IDisposable ManageExtension(Collider powerUpTrigger, string tagForPlayer, GameObject extenderPrefab, float timeToDespawn)
    {
        return powerUpTrigger
            .OnTriggerEnterAsObservable()
            .Where(collider => collider.gameObject.CompareTag(tagForPlayer))
            .Subscribe(collider =>
            {
                SpawnAndScheduleDestruction(extenderPrefab, collider.transform, timeToDespawn);
                Destroy(this.gameObject);
            });
    }

    private readonly Action<GameObject, Transform, float> SpawnAndScheduleDestruction =
        (prefab, parent, timeSpan) => { Destroy(Instantiate(prefab, parent, false), timeSpan); };
}
