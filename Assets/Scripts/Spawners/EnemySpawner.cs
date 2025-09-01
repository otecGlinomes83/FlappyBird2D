using Assets.Scripts.Spawners;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Enemy
{
    public class EnemySpawner : GenericSpawner<Enemy>
    {
        [SerializeField] private float _spawnRate;

        [SerializeField] private int _maxEnemyCount = 3;

        [SerializeField] private Game _game;
        [SerializeField] private Bird _bird;
        [SerializeField] private Enemy _enemyPrefab;
        [SerializeField] private BoxCollider2D _spawnZone;

        private Coroutine _spawnCoroutine;

        private void OnEnable()
        {
            _game.Started += StartSpawn;
            _game.Finished += Reset;
        }

        private void OnDisable()
        {
            _game.Started -= StartSpawn;
            _game.Finished -= Reset;
        }

        private void StartSpawn()
        {
            _spawnCoroutine = StartCoroutine(CooldownSpawn());
        }

        public override void Reset()
        {
            base.Reset();
            StopCoroutine(_spawnCoroutine);
        }

        protected override void OnGet(Enemy enemy)
        {
            base.OnGet(enemy);

            enemy.ReadyForRelease += _pool.Release;

            enemy.transform.position = new Vector2
                (
                _spawnZone.transform.position.x,
                Random.Range(_spawnZone.bounds.min.y, _spawnZone.bounds.max.y)
                );

            enemy.Initialize(_bird.transform);
        }

        protected override void OnRelease(Enemy enemy)
        {
            base.OnRelease(enemy);

            enemy.ReadyForRelease -= _pool.Release;
        }

        IEnumerator CooldownSpawn()
        {
            WaitForSeconds cooldown = new WaitForSeconds(_spawnRate);

            while (enabled)
            {
                yield return cooldown;

                if (_pool.CountActive < _maxEnemyCount)
                    _pool.Get();
            }
        }
    }
}