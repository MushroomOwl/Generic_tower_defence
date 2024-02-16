using UnityEngine;
using UnityEngine.UI;

namespace TD
{
    public class InputHandler : MonoSingleton<InputHandler>
    {
        private void Update()
        {
            ControlKeyboard();
        }

        private void ControlKeyboard()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                GameManager.TogglePause();
            }
        }
    }
}

