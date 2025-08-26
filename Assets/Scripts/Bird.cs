using Assets.Scripts;
using System;
using UnityEngine;

[RequireComponent(typeof(Mover))]
[RequireComponent(typeof(Shooter))]
public class Bird : MonoBehaviour
{
    [SerializeField] private CollisionDetector _collisionDetector;

    public event Action GameOver;
    private PlayerInput _playerInput;
    private Mover _mover;
    private Shooter _shooter;

    private void Awake()
    {
        _mover = GetComponent<Mover>();
        _shooter = GetComponent<Shooter>();

        _playerInput = new PlayerInput();
    }

    private void OnEnable()
    {
        _playerInput.Enable();
        _collisionDetector.Detected += OnCollisionDetected;
    }

    private void OnDisable()
    {
        _playerInput.Disable();
        _collisionDetector.Detected += OnCollisionDetected;
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
    }

    private void OnCollisionDetected()
    {
        GameOver?.Invoke();
    }
}
