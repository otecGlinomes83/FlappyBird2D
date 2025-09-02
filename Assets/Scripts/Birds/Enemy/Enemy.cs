using System;
using UnityEngine;

namespace Assets.Scripts.Enemy
{
    [RequireComponent(typeof(EnemyHealth))]
    [RequireComponent(typeof(CooldownWaiter))]
    [RequireComponent(typeof(EnemyShooter))]
    public class Enemy : MonoBehaviour, ResetAble, IShootAble
    {
        private EnemyHealth _health;
        private CooldownWaiter _cooldownWaiter;

        private Quaternion _lookLeft = Quaternion.Euler(0, 180, 0);

        private bool _dead = false;

        public event Action<Enemy> ReadyForRelease;

        public event Action ResetCompleted;
        public event Action ShootTriggered;

        private void Awake()
        {
            _health = GetComponent<EnemyHealth>();
            _cooldownWaiter = GetComponent<CooldownWaiter>();
        }

        private void Start()
        {
            transform.rotation = _lookLeft;
        }

        private void OnEnable()
        {
            _health.Dead += HandleDead;
            _cooldownWaiter.ShootAble += Shoot;

            _cooldownWaiter.TryStartWait();
        }

        private void OnDisable()
        {
            _health.Dead -= HandleDead;
            _cooldownWaiter.ShootAble -= Shoot;
        }

        public void Reset()
        {
            _dead = false;
            _cooldownWaiter.TryStopWait();
            ResetCompleted?.Invoke();
        }

        private void Shoot()
        {
            ShootTriggered?.Invoke();
        }

        private void HandleDead()
        {
            if (_dead)
                return;

            _dead = true;
            _cooldownWaiter.TryStopWait();
            ReadyForRelease?.Invoke(this);
        }
    }
}