using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using R3;
using R3.Triggers;

public class EnemyCollisions : MonoBehaviour
{
    [SerializeField] private string _tagForEnemies;
    public GameObject SCREEN_Restart;

    private void Start()
    {
        ManageEnemyInteractions(
            collider: GetComponent<Collider>(),
            player: GetComponent<PlayerMotionControl>(),
            auto: GetComponent<AutoPlayerMotion>(),
            tag: _tagForEnemies
            ).AddTo(this);
    }

    private IDisposable ManageEnemyInteractions(Collider collider, string tag, PlayerMotionControl player, AutoPlayerMotion auto)
    {
        return collider
            .OnCollisionEnterAsObservable()
            .Where(collision => collision.gameObject.CompareTag(tag))
            .Select<Collision, Action>(collision => () => { player.enabled = false; auto.enabled = false;
                SCREEN_Restart.SetActive(true); })
            .Subscribe(action => action());
    }
}
