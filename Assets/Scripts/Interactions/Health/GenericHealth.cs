using Assets.Scripts.Interfaces;
using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Interactions.Health
{
    public class GenericHealth<T> : MonoBehaviour where T : ResetAble
    {
        [SerializeField] private T _resetAble;

        [SerializeField] private float _maxHealth;

        public event Action Dead;

        private float _currentHealth;

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

        public void TakeDamage(float damage)
        {
            _currentHealth = Mathf.Max(_currentHealth - Mathf.Max(damage, 0f), 0f);

            if (_currentHealth <= 0)
                Dead?.Invoke();
        }
    }
}