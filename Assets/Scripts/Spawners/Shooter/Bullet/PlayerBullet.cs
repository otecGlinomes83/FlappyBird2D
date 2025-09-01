using UnityEngine;

public class PlayerBullet : Bullet
{
    protected override void TryAttack(Collider2D collider)
    {
        if (collider.gameObject.TryGetComponent(out EnemyHealth health))
            health.TakeDamage(Damage);

        StopAllCoroutines();
        ReleaseBullet();
    }
}
