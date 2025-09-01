using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    [RequireComponent(typeof(Shooter))]
    public class CooldownShooter : MonoBehaviour
    {
        [SerializeField] private float _attackRate;

        private Shooter _shooter;
        private Coroutine _cooldownShootCoroutine;
        private WaitForSeconds _cooldown;

        private void Awake()
        {
            _shooter = GetComponent<Shooter>();
        }

        public void TryStartShoot()
        {
            if (_cooldownShootCoroutine != null)
                TryStopShoot();

            _cooldownShootCoroutine = StartCoroutine(CooldownShoot());
        }

        public void TryStopShoot()
        {
            if (_cooldownShootCoroutine == null)
                return;

            StopCoroutine(_cooldownShootCoroutine);
            _cooldownShootCoroutine = null;
            _shooter.Reset();
        }

        private IEnumerator CooldownShoot()
        {
            _cooldown = new WaitForSeconds(_attackRate);

            while (enabled)
            {
                yield return _cooldown;

                _shooter.Shoot();
            }
        }
    }
}