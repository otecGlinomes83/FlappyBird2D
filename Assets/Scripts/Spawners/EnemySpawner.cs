using Assets.Scripts.Spawners;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Enemy
{
    public class EnemySpawner : GenericSpawner<Enemy>
    {
        [SerializeField] private float _spawnRate;

        [SerializeField] private int _maxEnemyCount = 3;

        [SerializeField] private Enemy _enemyPrefab;
        [SerializeField] private BoxCollider2D _spawnZone;
        [SerializeField] private EnemyDetector _enemyDetector;

        private Coroutine _spawnCoroutine;

        private void OnEnable()
        {
            _enemyDetector.Detected += Pool.Release;
        }

        private void OnDisable()
        {
            _enemyDetector.Detected -= Pool.Release;
        }

        public void TryStartSpawn()
        {
            if (_spawnCoroutine != null)
                return;

            _spawnCoroutine = StartCoroutine(SpawnPerCooldown());
        }

        public void TryStopSpawn()
        {
            if (_spawnCoroutine == null)
                return;

            StopCoroutine(_spawnCoroutine);

            Reset();
            _spawnCoroutine = null;
        }

        protected override void OnGet(Enemy enemy)
        {
            base.OnGet(enemy);

            enemy.transform.position = new Vector2
    (
    _spawnZone.transform.position.x,
    Random.Range(_spawnZone.bounds.min.y, _spawnZone.bounds.max.y)
    );

            enemy.ReadyForRelease += Pool.Release;
        }

        protected override void OnRelease(Enemy enemy)
        {
            enemy.Reset();
            base.OnRelease(enemy);

            enemy.ReadyForRelease -= Pool.Release;
        }

        private IEnumerator SpawnPerCooldown()
        {
            WaitForSeconds cooldown = new WaitForSeconds(_spawnRate);

            while (enabled)
            {
                yield return cooldown;

                if (Pool.CountActive < _maxEnemyCount)
                    Pool.Get();
            }
        }
    }
}