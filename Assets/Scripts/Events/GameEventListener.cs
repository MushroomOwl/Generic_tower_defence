using UnityEngine;

namespace TD
{
    public class GameEventListener : MonoBehaviour, IGameEventListener
    {
        [SerializeField] private GameEvent gameEvent;
        [SerializeField] private CustomEvent callback;

        private void OnEnable()
        {
            gameEvent?.Subscribe(this);
        }

        private void OnDisable()
        {
            gameEvent?.Unsubscribe(this);
        }

        public void OnRaiseEvent(Component caller, object data)
        {
            callback?.Invoke(caller, data);
        }
    }
}