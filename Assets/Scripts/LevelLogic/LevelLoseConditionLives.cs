using UnityEngine;

namespace TD
{
    public class LevelLoseConditionLives : LevelCondition, ILevelCondition
    {
        private bool _Fulfilled = false;

        public void UpdateCondition(Component caller, object data)
        {
            if (data is not int) {
                return;
            }

            int lives = (int)data;

            bool fulfilled = lives <= 0;

            if (_Fulfilled != fulfilled)
            {
                _Fulfilled = fulfilled;
                _ConditionValueChanged?.Invoke();
            }
        }

        public override bool Fulfilled => _Fulfilled;
    }
}