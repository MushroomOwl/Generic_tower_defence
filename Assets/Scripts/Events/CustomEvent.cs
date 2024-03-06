using UnityEngine;
using UnityEngine.Events;

namespace TD
{
    [System.Serializable]
    public class CustomEvent : UnityEvent<Component, object> { }
}
