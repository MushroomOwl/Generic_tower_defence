using UnityEngine;
using UnityEngine.SceneManagement;

namespace TD
{
    [CreateAssetMenu(fileName = "level_")]
    public class LevelProperties : ScriptableObject, IPersistantData
    {
        [SerializeField] private LevelStats _stats = new LevelStats();
        public LevelStats Stats => _stats;

        [SerializeField] private string _Title;
        public string Title => _Title;

        [SerializeField] private string _Scene;
        public string Scene => _Scene;

        [Header("Listening to events")]
        [SerializeField] private GameEvent _onLevelCleared;
        private SimpleListener _eventListener = new SimpleListener();
        

        private void OnEnable()
        {
            _eventListener.AddListener(CompleteLevelCallback);
            _onLevelCleared.Subscribe(_eventListener);
        }

        private void OnDisable()
        {
            _onLevelCleared.Unsubscribe(_eventListener);
        }

        private void CompleteLevelCallback()
        {
            string currentSceneName = SceneManager.GetActiveScene().name;
            if (currentSceneName != _Scene)
            {
                return;
            }
            // TODO: At the moment just completing level is ok, fix later
            _stats.CompleteLevel(LevelStats.CompletionGrade.Bronze, 0);
        }

        public string GetUniqueID()
        {
            return string.Concat(GetType().Name, name);
        }

        public string PackData()
        {
            return _stats.PackData();
        }

        public void UnpackData(string data)
        {
            _stats.UnpackData(data);
        }

        public void ResetState()
        {
            _stats = new LevelStats();
        }
    }
}
