using UnityEngine;

namespace TD
{
    public interface ICustomPrototype<T> where T : MonoBehaviour
    {
        T CloneSelf();
        void DestroySelf();
    }
}
