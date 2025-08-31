using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace Assets.Scripts
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private float _spawnRate;

        [SerializeField] private int _maxEnemyCount = 3;

        [SerializeField] private Game _game;
        [SerializeField] private Bird _bird;
        [SerializeField] private Enemy _enemyPrefab;
        [SerializeField] private BoxCollider2D _spawnZone;

        private Coroutine _spawnCoroutine;

        private ObjectPool<Enemy> _enemyPool;
        private List<Enemy> _enemies = new List<Enemy>();

        private void Awake()
        {
            _enemyPool = new ObjectPool<Enemy>
                (
                createFunc: () => CreateEnemy(),
                actionOnGet: (enemy) => SpawnEnemyRandomPosition(enemy),
                actionOnRelease: (enemy) => OnRelease(enemy)
                );
        }

        private void OnEnable()
        {
            _game.Started += StartSpawn;
            _game.Finished += StopSpawn;
        }

        private void OnDisable()
        {
            _game.Started -= StartSpawn;
            _game.Finished -= StopSpawn;
        }

        private void StartSpawn()
        {
            _spawnCoroutine = StartCoroutine(CooldownSpawn());
        }

        private void StopSpawn()
        {
            StopCoroutine(_spawnCoroutine);
            DestroyAll();

            _enemyPool.Clear();
            _enemies.Clear();
        }

        private void DestroyAll()
        {
            foreach (Enemy enemy in _enemies)
                Destroy(enemy.gameObject);
        }

        private Enemy CreateEnemy()
        {
            Enemy enemy = Instantiate(_enemyPrefab);
            _enemies.Add(enemy);

            return enemy;
        }

        private void OnRelease(Enemy enemy)
        {
            enemy.gameObject.SetActive(false);
            enemy.ReadyForRelease -= _enemyPool.Release;
        }

        private void SpawnEnemyRandomPosition(Enemy enemy)
        {
            enemy.gameObject.SetActive(true);
            enemy.ReadyForRelease += _enemyPool.Release;

            enemy.transform.position = new Vector2
                (
                _spawnZone.transform.position.x,
                Random.Range(_spawnZone.bounds.min.y, _spawnZone.bounds.max.y)
                );

            enemy.Initialize(_bird.transform);
        }

        IEnumerator CooldownSpawn()
        {
            WaitForSeconds cooldown = new WaitForSeconds(_spawnRate);

            while (enabled)
            {
                yield return cooldown;

                if (_enemyPool.CountActive < _maxEnemyCount)
                    _enemyPool.Get();
            }
        }
    }
}