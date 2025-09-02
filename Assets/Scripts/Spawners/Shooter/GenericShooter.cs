using UnityEngine;

namespace Assets.Scripts.Spawners.Shooter
{
    public class GenericShooter<TShootable, TBullet> : GenericSpawner<GenericBullet<TBullet>>
        where TShootable : IShootAble, ResetAble
        where TBullet : IDamageAbler
    {
        [SerializeField] protected Transform SpawnPosition;
        [SerializeField] protected TShootable ShootAble;

        protected void OnEnable()
        {
            ShootAble.ShootTriggered += Shoot;
            ShootAble.ResetCompleted += Reset;
        }

        protected void OnDisable()
        {
            ShootAble.ShootTriggered -= Shoot;
            ShootAble.ResetCompleted -= Reset;
            Reset();
        }

        protected void OnDrawGizmos()
        {
            Gizmos.color = Color.red;

            Gizmos.DrawRay(transform.position, transform.right * 10f);
        }

        protected void Shoot()
        {
            if (Pool.CountActive < MaxSpawns)
                Pool.Get();
        }

        protected override void OnRelease(GenericBullet<TBullet> bullet)
        {
            base.OnRelease(bullet);
            bullet.ReadyForRelease -= Pool.Release;
        }

        protected override void OnGet(GenericBullet<TBullet> bullet)
        {
            base.OnGet(bullet);
            bullet.ReadyForRelease += Pool.Release;

            bullet.transform.position = SpawnPosition.transform.position;
            bullet.Initialize(transform.right, transform.rotation);
        }
    }
}