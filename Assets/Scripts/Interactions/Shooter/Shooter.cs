using UnityEngine;
using UnityEngine.Pool;

namespace Assets.Scripts
{
    public class Shooter : MonoBehaviour
    {
        [SerializeField] private Bullet _bulletPrefab;
        [SerializeField] private Transform _spawnPosition;

        private ObjectPool<Bullet> _bulletPool;

        private void Awake()
        {
            _bulletPool = new ObjectPool<Bullet>
                (
                createFunc: () => Instantiate(_bulletPrefab),
                actionOnGet: (bullet) => OnGet(bullet),
                actionOnRelease: (bullet) => OnRelease(bullet)
                );
        }

        private void OnRelease(Bullet bullet)
        {
            bullet.gameObject.SetActive(false);
            bullet.ReadyForRelease -= _bulletPool.Release;
        }

        private void OnGet(Bullet bullet)
        {
            bullet.gameObject.SetActive(true);
            bullet.ReadyForRelease += _bulletPool.Release;

            bullet.transform.position = _spawnPosition.transform.position;
            bullet.Initialize(transform.right, transform.rotation);
        }

        public void Shoot()
        {
            _bulletPool.Get();
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;

            Gizmos.DrawRay(transform.position, transform.right * 10f);
        }
    }
}