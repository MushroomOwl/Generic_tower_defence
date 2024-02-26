using UnityEngine;

namespace TD
{
    public class LevelLoseConditionLives : LevelCondition
    {
        [SerializeField] private PlayerEventsBus _PlayerEvents;

        private void Awake()
        {
            _PlayerEvents.LivesChanged.AddListener(() => _ConditionValueChanged?.Invoke());
        }

        public override bool Fulfilled { get { return Player.NumLives <= 0; } }
    }
}