using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace TD
{
    public class LevelController : MonoBehaviour
    {
        [SerializeField] private GameEvent _OnLevelFailed;
        [SerializeField] private GameEvent _OnLevelCleared;

        [SerializeField] private LevelCondition[] _WinConditions;
        [SerializeField] private LevelCondition[] _LoseConditions;

        [SerializeField] private LevelProperties _LevelProps;

        public bool IsLevelFailed { get; private set; }
        public bool IsLevelCleared { get; private set; }

        private void Start()
        {
            IsLevelFailed = false;
            IsLevelCleared = false;

            foreach (var condition in _WinConditions)
            {
                condition.ConditionValueChanged.AddListener(CheckLevelCompletion);
            }
            foreach (var condition in _LoseConditions)
            {
                condition.ConditionValueChanged.AddListener(CheckLevelLosing);
            }
        }

        private void CheckLevelCompletion()
        {
            bool isCleared = _WinConditions.All(c => c.Fulfilled);

            if (isCleared && !IsLevelCleared)
            {
                IsLevelCleared = true;
                _OnLevelCleared?.Raise(this, null);
            }
        }

        private void CheckLevelLosing()
        {
            bool isFailed = _LoseConditions.Any(c => c.Fulfilled);

            if (isFailed && !IsLevelFailed)
            {
                IsLevelFailed = true;
                _OnLevelFailed?.Raise(this, null);
            }
        }
    }
}