using System.Collections.Generic;
using UnityEngine;

namespace TD
{
    [CreateAssetMenu(fileName = "e_")]
    public class GameEvent : ScriptableObject
    {
        private List<IGameEventListener> _Subscribers = new List<IGameEventListener>();

        public void Raise(Component caller, object data)
        {
            for (int i = 0; i < _Subscribers.Count; i++)
            {
                _Subscribers[i].OnRaiseEvent(caller, data);
            }
        }

        public void Subscribe(IGameEventListener listener)
        {
            if (_Subscribers.Contains(listener))
            {
                return;
            }
            _Subscribers.Add(listener);
        }

        public void Unsubscribe(IGameEventListener listener)
        {
            if (!_Subscribers.Contains(listener))
            {
                return;
            }
            _Subscribers.Remove(listener);
        }
    }
}