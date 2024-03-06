using UnityEngine;
using UnityEngine.Events;

namespace TD
{
    public class LevelCondition : MonoBehaviour, ILevelCondition
    {
        public virtual bool Fulfilled { get; }
        protected UnityEvent _ConditionValueChanged = new UnityEvent();
        public UnityEvent ConditionValueChanged => _ConditionValueChanged;
    }
}
