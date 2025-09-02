using UnityEngine;

public class BulletSpawner : GenericSpawner<Bullet>
{
    public int CountActive => Pool.CountActive;

    protected void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawRay(transform.position, transform.right * 10f);
    }

    public Bullet Shoot()
    {
        return Pool.Get();
    }

    protected override void OnRelease(Bullet bullet)
    {
        base.OnRelease(bullet);
        bullet.ReadyForRelease -= Pool.Release;
    }

    protected override void OnGet(Bullet bullet)
    {
        base.OnGet(bullet);
        bullet.ReadyForRelease += Pool.Release;
    }
}