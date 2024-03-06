using UnityEngine;

namespace TD
{
    public interface IGameEventListener
    {
        public void OnRaiseEvent(Component caller, object data);
    }
}
