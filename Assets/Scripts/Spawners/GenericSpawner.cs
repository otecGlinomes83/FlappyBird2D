using Assets.Scripts.Interfaces;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace Assets.Scripts.Spawners
{
    public abstract class GenericSpawner<T> : MonoBehaviour where T : Component
    {
        [SerializeField] protected int _maxSpawns;

        [SerializeField] protected Game _game;
        [SerializeField] protected T _prefab;

        protected ObjectPool<T> _pool;
        protected List<T> _objects = new List<T>();

        protected void Awake()
        {
            _pool = new ObjectPool<T>
                (
                createFunc: () => OnCreate(),
                actionOnGet: (obj) => OnGet(obj),
                actionOnRelease: (obj) => OnRelease(obj)
                );
        }

        protected void OnEnable()
        {

        }

        protected void OnDisable()
        {
            ClearAll();
        }

        protected T GetObject() =>
            _pool.Get();

        protected void ClearAll()
        {
            DestroyAll();

            _pool.Clear();
            _objects.Clear();
        }

        protected void OnGet(T obj)
        {
            obj.gameObject.SetActive(true);
        }

        protected T OnCreate()
        {
            T obj = Instantiate(_prefab);
            _objects.Add(obj);

            return obj;
        }

        protected void OnRelease(T obj)
        {
            obj.gameObject.SetActive(false);
        }

        protected void DestroyAll()
        {
            foreach (T obj in _objects)
                Destroy(obj.gameObject);
        }
    }
}