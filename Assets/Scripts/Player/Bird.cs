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
        _collisionDetector.Detected += Dead;
    }

    private void OnDisable()
    {
        _health.Dead -= Dead;
        _collisionDetector.Detected += Dead;
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
        _playerInput.Enable();
    }

    private void Dead()
    {
        GameOver?.Invoke();
        _playerInput.Disable();
    }
}
