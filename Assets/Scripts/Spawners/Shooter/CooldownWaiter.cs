using System;
using System.Collections;
using UnityEngine;

public class CooldownWaiter : MonoBehaviour
{
    [SerializeField] private float _attackRate;

    private Coroutine _cooldownShootCoroutine;
    private WaitForSeconds _cooldown;

    public event Action ShootAble;

    public void TryStartWait()
    {
        if (_cooldownShootCoroutine != null)
            TryStopWait();

        if (gameObject.activeInHierarchy)
            _cooldownShootCoroutine = StartCoroutine(ShootCooldown());
    }

    public void TryStopWait()
    {
        if (_cooldownShootCoroutine == null)
            return;

        StopCoroutine(_cooldownShootCoroutine);
        _cooldownShootCoroutine = null;
    }

    private IEnumerator ShootCooldown()
    {
        _cooldown = new WaitForSeconds(_attackRate);

        while (enabled)
        {
            yield return _cooldown;

            ShootAble?.Invoke();
        }
    }
}