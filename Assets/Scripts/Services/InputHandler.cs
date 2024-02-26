using UnityEngine;

namespace TD
{
    public class InputHandler : MonoSingleton<InputHandler>
    {
        [SerializeField] private InputBus _InputBus;

        private void Update()
        {
            KeyboardActions();
            MouseActions();
        }

        private void KeyboardActions()
        {
        }

        private void MouseActions()
        {
            if (Input.GetMouseButtonDown((int)Utilities.MouseButton.Left))
            {
                _InputBus.OnLMBClick();
            }

            if (Input.GetMouseButtonDown((int)Utilities.MouseButton.Right))
            {
                _InputBus.OnBuildCall();
            }
        }
    }
}

