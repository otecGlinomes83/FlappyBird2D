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
        }

        private IEnumerator CooldownShoot()
        {
            _cooldown = new WaitForSeconds(_attackRate);

            while (enabled)
            {
                _shooter.Shoot();

                yield return _cooldown;
            }
        }
    }
}