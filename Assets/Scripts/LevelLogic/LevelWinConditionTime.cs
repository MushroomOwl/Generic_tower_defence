using UnityEngine;

namespace TD
{
    [RequireComponent(typeof(EntityWithTimer))]
    public class LevelWinConditionTime : LevelCondition, ILevelCondition
    {
        [SerializeField] private GameEvent _OnLevelTimerUpdate;

        [SerializeField] private EntityWithTimer _TimerHandler;
        [SerializeField] private int _TimeTillEnd;

        private const string _WinConditionTimer = "winconditiontimer";
        private const string _OneSecondTick = "onesecondtick";

        private bool _Fulfilled = false;
        public override bool Fulfilled => _Fulfilled;

        private void Awake()
        {
            _TimerHandler = GetComponent<EntityWithTimer>();

            _TimerHandler.AddTimer(_WinConditionTimer, _TimeTillEnd);
            _TimerHandler.AddCallback(_WinConditionTimer, () =>
            {
                _Fulfilled = true;
                _TimerHandler.RemoveTimer(_WinConditionTimer);
                _ConditionValueChanged?.Invoke();
            });

            _TimerHandler.AddTimer(_OneSecondTick, 1);
            _TimerHandler.AddCallback(_OneSecondTick, () => _OnLevelTimerUpdate?.Raise(this, GetLevelTimeLeft()));
        }

        private void Start()
        {
            _OnLevelTimerUpdate?.Raise(this, GetLevelTimeLeft());
        }

        private float GetLevelTimeLeft()
        {
            return _TimerHandler.TimeLeft(_WinConditionTimer);
        }
    }
}