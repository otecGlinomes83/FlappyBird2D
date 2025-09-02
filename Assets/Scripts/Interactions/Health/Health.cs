using System;
using UnityEngine;

    public class Health : MonoBehaviour
    {
        [SerializeField] private int _maxHealth;

        private int _currentHealth;

        public event Action Dead;

        private void Start()
        {
            _currentHealth = _maxHealth;
        }

        public void Reset()
        {
            _currentHealth = _maxHealth;
        }

        public void TakeDamage(int damage)
        {
            _currentHealth = Mathf.Max(_currentHealth - Mathf.Max(damage, 0), 0);

            if (_currentHealth <= 0)
                Dead?.Invoke();
        }
    }
