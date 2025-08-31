using System;
using UnityEngine;

namespace Assets.Scripts.Detectors
{
    public class HealthDetector : MonoBehaviour
    {
        public event Action<Health> HealthDetected;

        [SerializeField] private BoxCollider2D _detectZone;

        private void Awake()
        {
            _detectZone = GetComponent<BoxCollider2D>();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.TryGetComponent(out Health health))
            {
                HealthDetected?.Invoke(health);
            }
        }
    }
}