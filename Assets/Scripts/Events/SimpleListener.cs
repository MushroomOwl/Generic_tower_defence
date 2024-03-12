using UnityEngine;
using System;

namespace TD
{
    public class SimpleListener : IGameEventListener
    {
        private Action _onEventAction;

        public void OnRaiseEvent(Component caller, object data)
        {
            if (_onEventAction != null)
            {
                _onEventAction();
            }
        }

        public void AddListener(Action action)
        {
            _onEventAction += action;
        }

        public void RemoveListener(Action action)
        {
            _onEventAction -= action;
        }
    }
}
