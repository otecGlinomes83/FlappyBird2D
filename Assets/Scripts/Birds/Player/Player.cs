using Assets.Scripts;
using Assets.Scripts.Interfaces;
using System;
using UnityEngine;

[RequireComponent(typeof(Mover))]
[RequireComponent(typeof(PlayerHealth))]
public class Player : MonoBehaviour, IShootAble, ResetAble
{
    [SerializeField] private CollisionDetector _collisionDetector;

    private PlayerHealth _health;
    private PlayerInput _playerInput;

    private bool _isDead = false;

    public event Action GameOver;
    public event Action FlyTriggered;
    public event Action ShootTriggered;
    public event Action ResetCompleted;

    private void Awake()
    {
        _health = GetComponent<PlayerHealth>();
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
            FlyTriggered?.Invoke();
        }

        if (_playerInput.Bird.Shoot.triggered)
        {
            ShootTriggered?.Invoke();
        }
    }

    public void Reset()
    {
        ResetCompleted?.Invoke();

        _playerInput.Enable();

        _isDead = false;
    }

    private void HandleCollision(Collider2D collider)
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
