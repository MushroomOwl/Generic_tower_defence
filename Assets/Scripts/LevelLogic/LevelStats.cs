using System;
using UnityEngine;

namespace TD
{
    [Serializable]
    public class LevelStats: IPackableData
    {
        public enum CompletionGrade
        {
            Bronze,
            Silver,
            Gold
        }

        [SerializeField] private bool _completed = false;
        [SerializeField] private int _scoreRecord = 0;
        [SerializeField] private CompletionGrade _grade = CompletionGrade.Bronze;
        [SerializeField] private bool _available = false;

        public bool Completed => _completed;
        public int ScoreRecord => _scoreRecord;
        public CompletionGrade Grade => _grade;
        public bool Available => _available;

        public string PackData()
        {
            return JsonUtility.ToJson(this);
        }

        public void UnpackData(string data)
        {
            LevelStats stats = JsonUtility.FromJson<LevelStats>(data);
            _completed = stats._completed;
            _scoreRecord = stats._scoreRecord;
            _grade = stats._grade;
            _available = stats._available;
        }

        public void CompleteLevel(CompletionGrade grade, int score)
        {
            if (score > _scoreRecord)
            {
                _scoreRecord = score;
            }

            if (grade > _grade)
            {
                _grade = grade;
            }

            _completed = true;
        }

        public void MakeAvailable()
        {
            _available = true;
        }
    }
}
