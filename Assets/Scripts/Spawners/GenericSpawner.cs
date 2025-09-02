using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

    public abstract class GenericSpawner<T> : MonoBehaviour where T : Component
    {
        [SerializeField] protected T Prefab;

        protected ObjectPool<T> Pool;
        protected List<T> Objects = new List<T>();

        protected void Awake()
        {
            Pool = new ObjectPool<T>
                (
                createFunc: () => OnCreate(),
                actionOnGet: (obj) => OnGet(obj),
                actionOnRelease: (obj) => OnRelease(obj)
                );
        }

        public void Reset()
        {
            DestroyAll();

            Pool.Clear();
            Objects.Clear();
        }

        protected virtual void OnGet(T obj)
        {
            obj.gameObject.SetActive(true);
        }

        protected T OnCreate()
        {
            T obj = Instantiate(Prefab);
            Objects.Add(obj);

            return obj;
        }

        protected virtual void OnRelease(T obj)
        {
            obj.gameObject.SetActive(false);
        }

        protected virtual void DestroyAll()
        {
            if (Objects == null)
                return;

            foreach (T obj in Objects)
            {
                if (obj != null)
                    Destroy(obj.gameObject);
            }
        }
    }