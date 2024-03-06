using UnityEngine;
using UnityEngine.SceneManagement;

namespace TD
{
    public class GameManager : MonoBehaviour
    {
        private static string _MainMenuSceneName = "MainMenu";

        [SerializeField] private GameEvent _OnPauseGame;

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
            ResumeTime();
            SceneManager.LoadScene(name);
        }

        public void RestartCurrentLevel()
        {
            LoadLevel(SceneManager.GetActiveScene().name);
        }

        public void LoadMainMenu()
        {
            ResumeTime();
            SceneManager.LoadScene(_MainMenuSceneName);
        }

        public void ExitGame()
        {
            Application.Quit();
        }
    }
}