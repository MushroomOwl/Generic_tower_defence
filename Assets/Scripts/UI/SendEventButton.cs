using System.Linq;
using UnityEditor;
using UnityEngine;

namespace TD
{
    public class SendEventButton : MonoBehaviour
    {
        [SerializeField] private GameEvent _onSomething;

        public void ButtonPressCallback()
        {
            _onSomething?.Raise(this, null);
        }
    }
}
