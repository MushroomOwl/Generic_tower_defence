using UnityEngine;
using UnityEngine.Events;

namespace TD
{
    public interface ILevelCondition
    {
        bool Fulfilled { get; }
        UnityEvent ConditionValueChanged { get; }
    }
}
