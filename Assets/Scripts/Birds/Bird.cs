using System;
using UnityEngine;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Shooter))]
public abstract class Bird : MonoBehaviour
{
    protected Health Health;
    protected Shooter Shooter;

    protected bool IsDead = false;

    protected virtual void Awake()
    {
        Health = GetComponent<Health>();
        Shooter = GetComponent<Shooter>();
    }

    protected virtual void OnEnable()
    {
        Health.Dead += HandleDead;
    }

    protected virtual void OnDisable()
    {
        Health.Dead -= HandleDead;
    }

    public virtual void Reset()
    {
        IsDead = false;

        Health.Reset();
        Shooter.Reset();
    }

    protected virtual void HandleDead()
    {
        if (IsDead)
            return;

        IsDead = true;
    }
}
