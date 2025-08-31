using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace Assets.Scripts
{
    public class Shooter : MonoBehaviour
    {
        [SerializeField] private int _maxShoots = 10;

        [SerializeField] private Bullet _bulletPrefab;
        [SerializeField] private Transform _spawnPosition;

        private ObjectPool<Bullet> _bulletPool;
        private List<Bullet> _bullets = new List<Bullet>();

        private void Awake()
        {
            _bulletPool = new ObjectPool<Bullet>
                (
                createFunc: () => CreateBullet(),
                actionOnGet: (bullet) => GetBullet(bullet),
                actionOnRelease: (bullet) => ReleaseBullet(bullet)
                );
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;

            Gizmos.DrawRay(transform.position, transform.right * 10f);
        }

        private Bullet CreateBullet()
        {
            Bullet bullet = Instantiate(_bulletPrefab);
            _bullets.Add(bullet);

            return bullet;
        }

        private void ReleaseBullet(Bullet bullet)
        {
            bullet.gameObject.SetActive(false);
            bullet.ReadyForRelease -= _bulletPool.Release;
        }

        private void GetBullet(Bullet bullet)
        {
            bullet.gameObject.SetActive(true);

            bullet.ReadyForRelease += _bulletPool.Release;

            bullet.transform.position = _spawnPosition.transform.position;
            bullet.Initialize(transform.right, transform.rotation);
        }

        public void Shoot()
        {
            if (_bulletPool.CountActive < _maxShoots)
                _bulletPool.Get();
        }

        public void Reset()
        {
            _bulletPool.Clear();

            if (_bullets == null)
                return;

            foreach (Bullet bullet in _bullets)
            {
                if (bullet != null)
                    Destroy(bullet.gameObject);
            }

            _bullets.Clear();
        }
    }
}