using System.Linq;
using UnityEngine;

namespace TD
{
    /// <summary>
    /// Component that handles global level events on scene and manages conditions to fail or complete level
    /// </summary>
    public class LevelController : MonoBehaviour
    {
        [SerializeField] private GameEvent _onLevelFailed;
        [SerializeField] private GameEvent _onLevelCleared;

        [SerializeField] private LevelCondition[] _completionConditions;
        [SerializeField] private LevelCondition[] _failureConditions;

        [SerializeField] private LevelProperties _levelProps;

        public bool IsLevelFailed { get; private set; }
        public bool IsLevelCleared { get; private set; }

        private void Start()
        {
            IsLevelFailed = false;
            IsLevelCleared = false;

            // Attach event listeners to conditions
            foreach (var condition in _completionConditions)
            {
                condition.ConditionValueChanged.AddListener(CheckLevelCompletion);
            }
            foreach (var condition in _failureConditions)
            {
                condition.ConditionValueChanged.AddListener(CheckLevelFailure);
            }
        }

        private void OnDestroy()
        {
            // Detach event listeners to avoid memory leaks
            foreach (var condition in _completionConditions)
            {
                condition.ConditionValueChanged.RemoveListener(CheckLevelCompletion);
            }
            foreach (var condition in _failureConditions)
            {
                condition.ConditionValueChanged.RemoveListener(CheckLevelFailure);
            }
        }

        /// <summary>
        /// Here, completion checker conditions are evaluated through AND operator.
        /// Only if all conditions are met, the level is considered completed.
        /// </summary>
        private void CheckLevelCompletion()
        {
            bool isCleared = _completionConditions.All(c => c.Fulfilled);
            if (isCleared && !IsLevelCleared)
            {
                IsLevelCleared = true;
                _onLevelCleared?.Raise(this, null);
            }
        }

        /// <summary>
        /// Here, failure checker conditions are evaluated through OR operator.
        /// If any failure occurs, the level is considered failed.
        /// </summary>
        private void CheckLevelFailure()
        {
            bool isFailed = _failureConditions.Any(c => c.Fulfilled);
            if (isFailed && !IsLevelFailed)
            {
                IsLevelFailed = true;
                _onLevelFailed?.Raise(this, null);
            }
        }
    }
}