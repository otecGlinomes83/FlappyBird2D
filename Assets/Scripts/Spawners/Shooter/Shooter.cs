using UnityEngine;

[RequireComponent(typeof(BulletSpawner))]
public class Shooter : MonoBehaviour
{
    [SerializeField] private int _maxShoots;
    [SerializeField] private Transform _spawnPosition;

    private BulletSpawner _bulletSpawner;

    private void Awake()
    {
        _bulletSpawner = GetComponent<BulletSpawner>();
    }

    public void Reset()
    {
        _bulletSpawner.Reset();
    }

    public void Shoot()
    {
        if (_bulletSpawner.CountActive < _maxShoots)
        {
            Bullet bullet = _bulletSpawner.Shoot();

            bullet.transform.position = _spawnPosition.transform.position;
            bullet.Initialize(transform.right, transform.rotation);
        }
    }
}