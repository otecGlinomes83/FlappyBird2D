using System;
using UnityEngine;

[RequireComponent(typeof(CooldownWaiter))]
public class Enemy : Bird
{
    private CooldownWaiter _cooldownWaiter;

    private Quaternion _lookLeft = Quaternion.Euler(0, 180, 0);

    public event Action<Enemy> Dead;

    protected override void Awake()
    {
        base.Awake();
        _cooldownWaiter = GetComponent<CooldownWaiter>();
    }

    private void Start()
    {
        transform.rotation = _lookLeft;
    }

    protected override void OnEnable()
    {
        _cooldownWaiter.ShootAble += Shooter.Shoot;

        _cooldownWaiter.TryStartWait();
    }

    protected override void OnDisable()
    {
        _cooldownWaiter.ShootAble -= Shooter.Shoot;
    }

    public override void Reset()
    {
        base.Reset();
        _cooldownWaiter.TryStopWait();
    }

    protected override void HandleDead()
    {
        _cooldownWaiter.TryStopWait();
        Dead?.Invoke(this);
    }
}