using UnityEngine;
using System.Collections.Generic;

namespace TD
{
    [CreateAssetMenu(fileName = "event_", menuName = "ScriptableObjects/GameEvent", order = 0)]
    public class GameEvent : ScriptableObject
    {
        private List<IGameEventListener> _subscribers = new List<IGameEventListener>();
        private List<IGameEventListener> _subscribersSnapshot = new List<IGameEventListener>();

        /// <summary>
        /// Raises the event, notifying all subscribers with the provided caller and data.
        /// Iterates over a snapshot to avoid issues if subscribers modify the list during their event response.
        /// </summary>
        public void Raise(Component caller, object data)
        {
            _subscribersSnapshot.Clear();
            _subscribersSnapshot.AddRange(_subscribers);

            foreach (var subscriber in _subscribersSnapshot)
            {
                subscriber.OnRaiseEvent(caller, data);
            }
        }

        /// <summary>
        /// Subscribes a listener to this event. If the listener is already subscribed, it does not subscribe them again.
        /// </summary>
        public void Subscribe(IGameEventListener listener)
        {
            if (listener == null)
            {
                Debug.LogWarning("Attempted to subscribe a null listener to GameEvent.");
                return;
            }

            if (_subscribers.Contains(listener))
            {
                return;
            }

            _subscribers.Add(listener);
        }

        /// <summary>
        /// Unsubscribes a listener from this event. If the listener is not subscribed, nothing happens.
        /// </summary>
        public void Unsubscribe(IGameEventListener listener)
        {
            if (listener == null)
            {
                Debug.LogWarning("Attempted to unsubscribe a null listener from GameEvent.");
                return;
            }

            _subscribers.Remove(listener);
        }
    }
}