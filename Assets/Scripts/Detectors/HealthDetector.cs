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
            _detectZone.isTrigger = true;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out Health health))
            {
                HealthDetected?.Invoke(health);
            }
        }
    }
}