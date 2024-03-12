using TD;
using UnityEngine;

public class MainMenuHandler : MonoBehaviour
{
    [SerializeField] private SaveManager _saveManager;
    public SaveManager SaveManager => _saveManager;

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadGame()
    {
        _saveManager.Load();
    }
}
