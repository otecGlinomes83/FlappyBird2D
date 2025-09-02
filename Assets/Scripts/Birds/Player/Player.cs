using System;
using UnityEngine;

[RequireComponent(typeof(Mover))]
[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Shooter))]
public class Player : MonoBehaviour
{
    [SerializeField] private CollisionDetector _collisionDetector;

    private Health _health;
    private Mover _mover;
    private Shooter _shooter;
    private PlayerInput _playerInput;

    private bool _isDead = false;

    public event Action Dead;

    private void Awake()
    {
        _health = GetComponent<Health>();
        _mover = GetComponent<Mover>();
        _shooter = GetComponent<Shooter>();

        _playerInput = new PlayerInput();
    }

    private void OnEnable()
    {
        _health.Dead += HandleDead;
        _collisionDetector.Detected += HandleCollision;
        _playerInput.Bird.Fly.performed += OnFly;
        _playerInput.Bird.Shoot.performed += OnShoot;
    }

    private void OnDisable()
    {
        _health.Dead -= HandleDead;
        _collisionDetector.Detected -= HandleCollision;
    }

    public void Reset()
    {
        _mover.Reset();
        _shooter.Reset();

        _playerInput.Enable();

        _isDead = false;
    }

    private void OnShoot(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        _shooter.Shoot();
    }

    private void OnFly(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        _mover.Fly();
    }

    private void HandleCollision(Collider2D collider)
    {
        HandleDead();
    }

    private void HandleDead()
    {
        if (_isDead)
            return;

        _isDead = true;
        Dead?.Invoke();
        _playerInput.Disable();
    }
}
