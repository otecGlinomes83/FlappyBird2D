using System;
using UnityEngine;

[RequireComponent(typeof(Mover))]
public class Player : Bird
{
    [SerializeField] private CollisionDetector _collisionDetector;

    private Mover _mover;
    private PlayerInput _playerInput;

    public event Action Dead;

    protected override void Awake()
    {
        base.Awake();
        _mover = GetComponent<Mover>();
        _playerInput = new PlayerInput();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        _collisionDetector.Detected += HandleCollision;
        _playerInput.Bird.Fly.performed += OnFly;
        _playerInput.Bird.Shoot.performed += OnShoot;
    }

    protected override void OnDisable()
    {
        base.OnDisable();

        _playerInput.Bird.Fly.performed -= OnFly;
        _playerInput.Bird.Shoot.performed -= OnShoot;
        _collisionDetector.Detected -= HandleCollision;
    }

    public override void Reset()
    {
        base.Reset();
        _mover.Reset();
        _playerInput.Enable();
    }

    private void OnShoot(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        Shooter.Shoot();
    }

    private void OnFly(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        _mover.Fly();
    }

    private void HandleCollision(Collider2D collider)
    {
        HandleDead();
        Dead?.Invoke();
    }

    protected override void HandleDead()
    {
        _playerInput.Disable();
    }
}
