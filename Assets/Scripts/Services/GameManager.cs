using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TD
{
    public class GameManager : MonoBehaviour
    {
        private static string _MainMenuSceneName = "MainMenu";

        [SerializeField] private GameEvent _OnPauseGame;
        [SerializeField] private GameEvent _OnLevelLoad;

        private bool _initiateSceneLoad = false;
        private Action _sceneLoadAction = null;

        public void StopTime()
        {
            Time.timeScale = 0;
        }

        public void FinishLevel()
        {
            Debug.Log("Not Implemented");
        }

        public void FailLevel()
        {
            Debug.Log("Not Implemented");
        }

        public void ResumeTime()
        {
            Time.timeScale = 1;
        }

        public void TogglePause()
        {
            if (Time.timeScale == 0)
            {
                ResumeTime();
            }
            else
            {
                StopTime();
            }
        }

        public void LoadLevel(string name)
        {
            _sceneLoadAction = () => SceneManager.LoadScene(name);
            _initiateSceneLoad = true;
        }

        private void LateUpdate()
        {
            if (!_initiateSceneLoad)
            {
                return;
            }

            _OnLevelLoad?.Raise(this, null);
            _sceneLoadAction();
            ResumeTime();
        }

        public void RestartCurrentLevel()
        {
            LoadLevel(SceneManager.GetActiveScene().name);
        }

        public void LoadMainMenu()
        {
            LoadLevel(_MainMenuSceneName);
        }

        public void ExitGame()
        {
            Application.Quit();
        }
    }
}