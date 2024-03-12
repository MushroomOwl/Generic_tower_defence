using UnityEditor;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

namespace TD
{
    [CreateAssetMenu(fileName = "SaveManager")]
    public class SaveManager: ScriptableObject
    {
        private SavesHandler _savesHandler;

        [Header("Listening to events")]
        [SerializeField] private GameEvent _onNewGameStart;
        private SimpleListener _resetDataListener = new SimpleListener();

        [SerializeField] private GameEvent _onSaveGameCall;
        private SimpleListener _saveGameListener = new SimpleListener();

        private void OnEnable()
        {
            IFileManager fileManager = new SyncFileManager();
            _savesHandler = new SavesHandler(fileManager);

            _resetDataListener.AddListener(ResetData);
            _onNewGameStart.Subscribe(_resetDataListener);

            _saveGameListener.AddListener(Save);
            _onSaveGameCall.Subscribe(_saveGameListener);
        }

        private void OnDisable()
        {
            _savesHandler.Save();            
        }

        public void Save()
        {
            _savesHandler.Save();
        }

        public void Load()
        {
            _savesHandler.Load();
        }

        public void ResetData()
        {
            _savesHandler.ResetData();
        }
    }
}
