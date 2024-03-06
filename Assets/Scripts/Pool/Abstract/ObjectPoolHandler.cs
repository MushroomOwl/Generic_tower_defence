using UnityEngine;
using UnityEngine.Pool;

namespace TD
{
    public class ObjectPoolHandler<T>: ScriptableObject where T : MonoBehaviour, IPoolable<T>
    {
        private ObjectPool<T> _pool;
        [SerializeField] private T _prefab;
        [SerializeField] private bool _enabled;
        [SerializeField] private int _capacity = 10;
        [SerializeField] private int _maxSize = 10000;

        public T PoolStantiate()
        {
            return _pool.Get();
        }

        public void PoolStroy(T instance)
        {
            _pool.Release(instance);
        }

        public void OnEnable()
        {
            _pool = new ObjectPool<T>(CreateInstance, OnGetFromPool,OnReleaseToPool, OnDestroyInPool, true, _capacity, _maxSize);
        }

        private T CreateInstance()
        {
            T instance = Instantiate(_prefab);
            return instance;
        }

        private void OnGetFromPool(T instance)
        {
            instance.OnGetFromPool();
        }
        private void OnReleaseToPool(T instance)
        {
            instance.OnReleaseToPool();
        }

        private void OnDestroyInPool(T instance)
        {
            instance.OnDestroyInPool();
        }
    }
}
