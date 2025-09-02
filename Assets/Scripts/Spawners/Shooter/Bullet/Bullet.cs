using System;
using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private int Damage;
    [SerializeField] private float Speed;
    [SerializeField] private float LifeTime = 1f;

    public event Action<Bullet> ReadyForRelease;

    private CollisionDetector _detector;

    private Vector2 _direction;

    private bool _isActive;

    private void Awake()
    {
        _detector = GetComponent<CollisionDetector>();
    }

    private void OnEnable()
    {
        _detector.Detected += TryAttack;
    }

    private void OnDisable()
    {
        _detector.Detected -= TryAttack;
    }

    public void Initialize(Vector2 direction, Quaternion rotation)
    {
        _direction = direction;
        transform.rotation = rotation;
        _isActive = true;

        StartCoroutine(Move());
    }

    private void ReleaseBullet() =>
        ReadyForRelease?.Invoke(this);

    private void TryAttack(Collider2D collider2D)
    {
        if (collider2D.gameObject.TryGetComponent(out Health health))
            health.TakeDamage(Damage);

        StopAllCoroutines();
        ReleaseBullet();
    }

    private IEnumerator Move()
    {
        StartCoroutine(WaitLifeTime());

        while (_isActive)
        {
            yield return null;
            transform.Translate(_direction.normalized * Speed * Time.deltaTime);
        }

        ReadyForRelease?.Invoke(this);
    }

    private IEnumerator WaitLifeTime()
    {
        yield return new WaitForSeconds(LifeTime);

        _isActive = false;
    }
}