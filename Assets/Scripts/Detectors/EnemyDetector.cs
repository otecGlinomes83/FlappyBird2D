using System;
using UnityEngine;

namespace Assets.Scripts.Enemy
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class EnemyDetector : MonoBehaviour
    {
        public event Action<Enemy> EnemyDetected;

        private BoxCollider2D _detectZone;

        private void Awake()
        {
            _detectZone = GetComponent<BoxCollider2D>();
            _detectZone.isTrigger = true;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out Enemy enemy))
            {
                EnemyDetected?.Invoke(enemy);
            }
        }
    }
}