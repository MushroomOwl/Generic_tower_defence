using UnityEngine;

namespace TD
{
    public class InputHandler : MonoSingleton<InputHandler>
    {
        [SerializeField] private GameEvent _OnLMBClick;
        [SerializeField] private GameEvent _OnBuildGridCall;
        [SerializeField] private GameEvent _OnPauseCall;

        private void Update()
        {
            KeyboardActions();
            MouseActions();
        }

        private void KeyboardActions()
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                _OnPauseCall?.Raise(this, null);
            }
        }

        private void MouseActions()
        {
            if (Input.GetMouseButtonDown((int)Utilities.MouseButton.Left))
            {
                _OnLMBClick?.Raise(this, null);
            }

            if (Input.GetMouseButtonDown((int)Utilities.MouseButton.Right))
            {
                _OnBuildGridCall?.Raise(this, null);
            }
        }
    }
}

