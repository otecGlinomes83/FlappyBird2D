using System;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class GenericDetector<T> : MonoBehaviour where T : Component
{
    private BoxCollider2D _detectZone;

    public event Action<T> Detected;

    protected void Awake()
    {
        _detectZone = GetComponent<BoxCollider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out T obj))
            Detected?.Invoke(obj);
    }
}