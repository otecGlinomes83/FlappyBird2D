using Assets.Scripts.Interfaces;
using UnityEngine;

namespace Assets.Scripts.Spawners.Shooter
{
    public class GenericShooter<T> : GenericSpawner<Bullet> where T : IShootAble, ResetAble
    {
        [SerializeField] protected Transform _spawnPosition;
        [SerializeField] protected T _shootAble;

        protected void OnEnable()
        {
            _shootAble.ShootTriggered += Shoot;
            _shootAble.ResetCompleted += Reset;
        }

        protected void OnDisable()
        {
            _shootAble.ShootTriggered -= Shoot;
            _shootAble.ResetCompleted -= Reset;
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

        protected override void OnRelease(Bullet bullet)
        {
            base.OnRelease(bullet);
            bullet.ReadyForRelease -= Pool.Release;
        }

        protected override void OnGet(Bullet bullet)
        {
            base.OnGet(bullet);
            bullet.ReadyForRelease += Pool.Release;

            bullet.transform.position = _spawnPosition.transform.position;
            bullet.Initialize(transform.right, transform.rotation);
        }
    }
}