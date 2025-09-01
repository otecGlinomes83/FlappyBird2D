using Assets.Scripts;
using System;
using UnityEngine;

[RequireComponent(typeof(Mover))]
[RequireComponent(typeof(Shooter))]
[RequireComponent(typeof(Health))]
public class Bird : MonoBehaviour
{
    [SerializeField] private CollisionDetector _collisionDetector;

    public event Action GameOver;

    private Health _health;
    private PlayerInput _playerInput;
    private Mover _mover;
    private Shooter _shooter;

    private bool _isDead = false;

    private void Awake()
    {
        _mover = GetComponent<Mover>();
        _shooter = GetComponent<Shooter>();
        _health = GetComponent<Health>();
        _playerInput = new PlayerInput();
    }

    private void OnEnable()
    {
        _health.Dead += Dead;
        _collisionDetector.Detected += HandleCollision;
    }

    private void OnDisable()
    {
        _health.Dead -= Dead;
        _collisionDetector.Detected -= HandleCollision;
    }

    private void Update()
    {
        if (_playerInput.Bird.Fly.triggered)
        {
            _mover.Fly();
        }

        if (_playerInput.Bird.Shoot.triggered)
        {
            _shooter.Shoot();
        }
    }

    public void Reset()
    {
        _mover.Reset();
        _shooter.Reset();
        _health.Reset();

        _playerInput.Enable();

        _isDead = false;
    }

    private void HandleCollision(Collision2D collision)
    {
        Dead();
    }

    private void Dead()
    {
        if (_isDead)
            return;

        _isDead = true;
        GameOver?.Invoke();
        _playerInput.Disable();
    }
}
