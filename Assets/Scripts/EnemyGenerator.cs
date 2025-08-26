using UnityEngine;
using UnityEngine.Pool;

namespace Assets.Scripts
{
    public class EnemyGenerator : MonoBehaviour
    {
        [SerializeField] private Enemy _enemyPrefab;
        [SerializeField] private float _spawnRate;
        [SerializeField] private BoxCollider2D _spawnZone;
        [SerializeField] private EnemyDetector _enemyDetector;

        private ObjectPool<Enemy> _enemyPool;

        private void Awake()
        {
            _enemyPool = new ObjectPool<Enemy>
                (
                createFunc: () => OnCreate(),
                actionOnGet: (enemy) => SpawnEnemyRandomPosition(enemy),
                actionOnRelease: (enemy) => OnRelease(enemy)
                );
        }

        private void OnEnable()
        {
            _enemyDetector.EnemyDetected += ReleaseEnemy;
        }

        private void OnDisable()
        {
            _enemyDetector.EnemyDetected -= ReleaseEnemy;
        }

        private void ReleaseEnemy(Enemy enemy)
        {
            _enemyPool.Release(enemy);
        }

        private void OnRelease(Enemy enemy)
        {
            enemy.gameObject.SetActive(false);
        }

        private void GetEnemy()
        {
            Enemy enemy = _enemyPool.Get();
        }

        private void SpawnEnemyRandomPosition(Enemy enemy)
        {
            enemy.transform.position = new Vector2
                (
                _spawnZone.transform.position.x,
                Random.Range(_spawnZone.bounds.min.y, _spawnZone.bounds.max.y)
                );
        }

        private Enemy OnCreate()
        {
            return Instantiate(_enemyPrefab);
        }
    }
}