using Assets.Scripts;
using System;
using System.Collections;
using UnityEngine;

public class EnemyBullet : Bullet
{
    protected override void TryAttack(Collider2D collider)
    {
        if (collider.gameObject.TryGetComponent(out PlayerHealth health))
            health.TakeDamage(Damage);

        StopAllCoroutines();

        ReleaseBullet();
    }
}
