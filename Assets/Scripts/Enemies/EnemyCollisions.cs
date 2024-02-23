using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using R3;
using R3.Triggers;

public class EnemyCollisions : MonoBehaviour
{
    [SerializeField] private string _tagForEnemies;

    private void Start()
    {
        ManageEnemyInteractions(
            collider: GetComponent<Collider>(),
            player:GetComponent<PlayerMotionControl>(),
            tag: _tagForEnemies
            ).AddTo(this);
    }

    private IDisposable ManageEnemyInteractions(Collider collider, string tag, PlayerMotionControl player)
    {
        return collider
            .OnCollisionEnterAsObservable()
            .Where(collision => collision.gameObject.CompareTag(tag))
            .Select<Collision, Action>(collision => () => { player.enabled = false; })
            .Subscribe(action => action());
    }
}
