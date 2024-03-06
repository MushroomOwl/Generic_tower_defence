using UnityEngine;

namespace TD
{
    public class PersistantGameEventListener : MonoBehaviour, IGameEventListener
    {
        [SerializeField] private GameEvent gameEvent;
        [SerializeField] private CustomEvent callback;

        private void Awake()
        {
            gameEvent?.Subscribe(this);
        }

        private void OnDestroy()
        {
            gameEvent?.Unsubscribe(this);
        }

        public void OnRaiseEvent(Component caller, object data)
        {
            callback?.Invoke(caller, data);
        }
    }
}