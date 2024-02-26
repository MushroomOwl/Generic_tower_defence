using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

namespace TD
{
    [CreateAssetMenu(fileName = "InputBus")]
    public class InputBus: ScriptableObject
    {
        [SerializeField] private UnityEvent _LMBClick;
        public UnityEvent LMBClick => _LMBClick;

        [SerializeField] private UnityEvent _BuildCall;
        public UnityEvent BuildCall => _BuildCall;

        public void OnLMBClick()
        {
            _LMBClick.Invoke();
        }

        public void OnBuildCall()
        {
            BuildCall.Invoke();
        }
    }
}
