using System;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.SceneManagement;

namespace TD
{
    public class ObjectPoolHandler<T> : ScriptableObject where T : MonoBehaviour, IPoolable<T>
    {
        private class SimpleListener : IGameEventListener
        {
            private Action _onEventAction;

            public SimpleListener(Action action)
            {
                _onEventAction = action;
            }

            public void OnRaiseEvent(Component caller, object data)
            {
                _onEventAction();
            }
        }

        private ObjectPool<T> _pool;
        private SimpleListener _levelEndListener;
        [SerializeField] private T _prefab;
        [SerializeField] private bool _enabled;
        [SerializeField] private int _capacity = 10;
        [SerializeField] private int _maxSize = 10000;
        [SerializeField] private GameEvent _onLevelLoad;

        public void OnEnable()
        {
            _pool = new ObjectPool<T>(CreateInstance, OnGetFromPool, OnReleaseToPool, OnDestroyInPool, true, _capacity, _maxSize);

            _levelEndListener = new SimpleListener(Flush);
            _onLevelLoad.Subscribe(_levelEndListener);
        }

        private void OnDisable()
        {
            _onLevelLoad.Unsubscribe(_levelEndListener);
        }

        public T PoolStantiate()
        {
            return _pool.Get();
        }

        public void PoolStroy(T instance)
        {
            _pool.Release(instance);
        }

        private void Flush()
        {
            _pool.Dispose();
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
