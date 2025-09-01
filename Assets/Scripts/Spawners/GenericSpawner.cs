using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace Assets.Scripts.Spawners
{
    public abstract class GenericSpawner<T> : MonoBehaviour where T : Component
    {
        [SerializeField] protected int _maxSpawns;

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

        public virtual void Reset()
        {
            DestroyAll();

            _pool.Clear();
            _objects.Clear();
        }

        protected virtual void OnGet(T obj)
        {
            obj.gameObject.SetActive(true);
        }

        protected virtual T OnCreate()
        {
            T obj = Instantiate(_prefab);
            _objects.Add(obj);

            return obj;
        }

        protected virtual void OnRelease(T obj)
        {
            obj.gameObject.SetActive(false);
        }

        protected virtual void DestroyAll()
        {
            if (_objects == null)
                return;

            foreach (T obj in _objects)
                Destroy(obj.gameObject);
        }
    }
}