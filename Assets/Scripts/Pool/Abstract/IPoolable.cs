using UnityEngine;

namespace TD
{
    public interface IPoolable<T> where T : MonoBehaviour
    {
        void OnGetFromPool();
        void OnReleaseToPool();
        void OnDestroyInPool();
    }
}
