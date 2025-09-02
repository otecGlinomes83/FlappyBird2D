using System;
using UnityEngine;

namespace Assets.Scripts.Interactions.Health
{
    public class GenericHealth<T> : MonoBehaviour, IDamageAbler where T : ResetAble
    {
        [SerializeField] private T _resetAble;

        [SerializeField] private int _maxHealth;

        public event Action Dead;

        private int _currentHealth;

        private void Start()
        {
            _currentHealth = _maxHealth;
        }

        private void OnEnable()
        {
            _resetAble.ResetCompleted += Reset;
        }

        private void OnDisable()
        {
            _resetAble.ResetCompleted -= Reset;
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
}