using Assets.Scripts.Detectors;
using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    [RequireComponent(typeof(HealthDetector))]
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private float _damage;
        [SerializeField] private float _speed;
        [SerializeField] private float _lifeTime = 1f;

        public event Action<Bullet> ReadyForRelease;

        private HealthDetector _detector;

        private Vector2 _direction;

        private bool _isActive;

        private void Awake()
        {
            _detector = GetComponent<HealthDetector>();
        }

        private void OnEnable()
        {
            _detector.HealthDetected += OnHealthDetected;
        }

        private void OnDisable()
        {
            _detector.HealthDetected -= OnHealthDetected;
        }

        public void Initialize(Vector2 direction, Quaternion rotation)
        {
            _direction = direction;
            transform.rotation = rotation;
            _isActive = true;

            StartCoroutine(Move());
        }
        private void OnHealthDetected(Health health)
        {
            health.TakeDamage(_damage);
            StopAllCoroutines();

            ReadyForRelease?.Invoke(this);
        }

        private IEnumerator Move()
        {
            StartCoroutine(WaitLifeTime());

            while (_isActive)
            {
                yield return null;
                transform.Translate(_direction.normalized * _speed * Time.deltaTime);
            }

            ReadyForRelease?.Invoke(this);
        }

        private IEnumerator WaitLifeTime()
        {
            yield return new WaitForSeconds(_lifeTime);

            _isActive = false;
        }
    }
}