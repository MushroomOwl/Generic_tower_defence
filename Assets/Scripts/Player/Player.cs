using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace TD
{
    public class Player : MonoSingleton<Player>
    {
        [SerializeField] private int _Score;
        [SerializeField] private int _KillCount;
        [SerializeField] private int _NumLives;

        private UnityEvent _ScoreChanged = new UnityEvent();
        private UnityEvent _KillsChanged = new UnityEvent();
        private UnityEvent _LivesChanged = new UnityEvent();

        public static UnityEvent ScoreChanged => _Instance._ScoreChanged;
        public static UnityEvent KillsChanged => _Instance._KillsChanged;
        public static UnityEvent LivesChanged => _Instance._LivesChanged;

        public static int Score => _Instance._Score;
        public static int KillCount => _Instance._KillCount;
        public static int NumLives => _Instance._NumLives;

        public static void AddKill()
        {
            _Instance._KillCount++;
            _Instance._KillsChanged?.Invoke();
        }

        public static void AddScore(int amount)
        {
            _Instance._Score += amount;
            _Instance._ScoreChanged?.Invoke();
        }
    }
}

