using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace TD
{
    [CreateAssetMenu(fileName="PlayerEventsBus")]
    public class PlayerEventsBus : ScriptableObject
    {
        [SerializeField] private UnityEvent _GoldChanged;
        public UnityEvent GoldChanged => _GoldChanged;

        [SerializeField] private UnityEvent _LivesChanged;
        public UnityEvent LivesChanged => _LivesChanged;

        public void OnGoldChanged()
        {
            _GoldChanged?.Invoke();
        }

        public void OnLivesChanged()
        {
            _LivesChanged?.Invoke();
        }
    }
}

