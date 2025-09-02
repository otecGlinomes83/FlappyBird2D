using System;
using UnityEngine;

    [RequireComponent(typeof(Health))]
    [RequireComponent(typeof(CooldownWaiter))]
    [RequireComponent(typeof(Shooter))]
    public class Enemy : MonoBehaviour
    {
        private Health _health;
        private CooldownWaiter _cooldownWaiter;
        private Shooter _shooter;

        private Quaternion _lookLeft = Quaternion.Euler(0, 180, 0);

        private bool _dead = false;

        public event Action<Enemy> ReadyForRelease;

        private void Awake()
        {
            _shooter = GetComponent<Shooter>();
            _health = GetComponent<Health>();
            _cooldownWaiter = GetComponent<CooldownWaiter>();
        }

        private void Start()
        {
            transform.rotation = _lookLeft;
        }

        private void OnEnable()
        {
            _health.Dead += HandleDead;
            _cooldownWaiter.ShootAble += _shooter.Shoot;

            _cooldownWaiter.TryStartWait();
        }

        private void OnDisable()
        {
            _health.Dead -= HandleDead;
            _cooldownWaiter.ShootAble -= _shooter.Shoot;
        }

        public void Reset()
        {
            _dead = false;
            _cooldownWaiter.TryStopWait();

            _health.Reset();
            _shooter.Reset();
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