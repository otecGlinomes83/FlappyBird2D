using Assets.Scripts.Spawners;
using UnityEngine;

namespace Assets.Scripts
{
    public class Shooter : GenericSpawner<Bullet>
    {
        [SerializeField] private Bullet _bulletPrefab;
        [SerializeField] private Transform _spawnPosition;

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;

            Gizmos.DrawRay(transform.position, transform.right * 10f);
        }

        public void Shoot()
        {
            if (_pool.CountActive < _maxSpawns)
                _pool.Get();
        }

        protected override void OnRelease(Bullet bullet)
        {
            base.OnRelease(bullet);
            bullet.ReadyForRelease -= _pool.Release;
        }

        protected override void OnGet(Bullet bullet)
        {
            base.OnGet(bullet);
            bullet.ReadyForRelease += _pool.Release;

            bullet.transform.position = _spawnPosition.transform.position;
            bullet.Initialize(transform.right, transform.rotation);
        }
    }
}