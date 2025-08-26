using TMPro;
using UnityEngine;

namespace Assets.Scripts
{
    [RequireComponent(typeof(Health))]
    [RequireComponent(typeof(CooldownShooter))]
    public class Enemy : MonoBehaviour
    {
        private Health _health;
        private CooldownShooter _cooldownShooter;

        private void Awake()
        {
            _health = GetComponent<Health>();
            _cooldownShooter = GetComponent<CooldownShooter>();
        }

        private void OnEnable()
        {
            _cooldownShooter.TryStartShoot();
        }

        private void OnDisable()
        {
            _cooldownShooter.TryStopShoot();
        }
    }
}