using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private float _maxHealth;

        public event Action Dead;

        private float _currentHealth;

        private void Start()
        {
            _currentHealth = _maxHealth;
        }

        public void TakeDamage(float damage)
        {
            _currentHealth = Mathf.Max(_currentHealth - Mathf.Max(damage, 0f), 0f);

            if (_currentHealth <= 0)
                Dead?.Invoke();
        }

        public void Reset()
        {
            _currentHealth = _maxHealth;
        }
    }
}