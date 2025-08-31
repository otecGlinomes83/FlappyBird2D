using Assets.Scripts.Interfaces;
using Assets.Scripts.Player;
using System;
using UnityEngine;

namespace Assets.Scripts
{
    [RequireComponent(typeof(Health))]
    [RequireComponent(typeof(CooldownShooter))]
    [RequireComponent(typeof(BirdTracker))]
    public class Enemy : MonoBehaviour, ReleaseAble<Enemy>
    {
        public event Action<Enemy> ReadyForRelease;

        private Health _health;
        private BirdTracker _birdTracker;
        private CooldownShooter _cooldownShooter;

        private Quaternion _lookLeft = Quaternion.Euler(0, 180, 0);

        public GameObject GameObject => gameObject;

        private void Awake()
        {
            _health = GetComponent<Health>();
            _birdTracker = GetComponent<BirdTracker>();
            _cooldownShooter = GetComponent<CooldownShooter>();
        }

        private void Start()
        {
            transform.rotation = _lookLeft;
        }

        private void OnEnable()
        {
            _health.Dead += HandleDead;
        }

        private void OnDisable()
        {
            _health.Dead -= HandleDead;
            _cooldownShooter.TryStopShoot();
        }

        public void Initialize(Transform target)
        {
            _birdTracker.SetTarget(target);
            _cooldownShooter.TryStartShoot();
        }

        private void HandleDead()
        {
            _cooldownShooter.TryStopShoot();
            ReadyForRelease?.Invoke(this);
        }
    }
}