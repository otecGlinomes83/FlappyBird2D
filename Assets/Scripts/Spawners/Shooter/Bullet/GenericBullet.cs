using Assets.Scripts;
using System;
using System.Collections;
using UnityEngine;

public abstract class GenericBullet<T> : MonoBehaviour where T: IDamageAbler
{
    [SerializeField] protected int Damage;
    [SerializeField] protected float Speed;
    [SerializeField] protected float LifeTime = 1f;

    public event Action<GenericBullet<T>> ReadyForRelease;

    private CollisionDetector _detector;

    private Vector2 _direction;

    private bool _isActive;

    protected virtual void Awake()
    {
        _detector = GetComponent<CollisionDetector>();
    }

    protected virtual void OnEnable()
    {
        _detector.Detected += TryAttack;
    }

    protected virtual void OnDisable()
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

    protected void ReleaseBullet() =>
        ReadyForRelease?.Invoke(this);

    protected void TryAttack(Collider2D collider2D)
    {
        if (collider2D.gameObject.TryGetComponent(out T health))
            health.TakeDamage(Damage);

        StopAllCoroutines();
        ReleaseBullet();
    }

    protected IEnumerator Move()
    {
        StartCoroutine(WaitLifeTime());

        while (_isActive)
        {
            yield return null;
            transform.Translate(_direction.normalized * Speed * Time.deltaTime);
        }

        ReadyForRelease?.Invoke(this);
    }

    protected IEnumerator WaitLifeTime()
    {
        yield return new WaitForSeconds(LifeTime);

        _isActive = false;
    }
}