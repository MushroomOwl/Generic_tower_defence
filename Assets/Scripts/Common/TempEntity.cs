using UnityEngine;
using UnityEngine.Events;

namespace TD
{
    public class TempEntity : EntityWithTimer
    {
        [SerializeField] private bool showtimer;

        private static string _TTLTimerName = "ttl";

        [SerializeField] private float _TTL;

        [SerializeField] private UnityEvent _OnTimerEnd;

        private void Awake()
        {
            AddTimer(_TTLTimerName, _TTL);
            AddCallback(_TTLTimerName, () => EOL());
        }

        private void EOL()
        {
            _OnTimerEnd.Invoke();
        }

        protected void ResetTTL()
        {
            ResetTimer(_TTLTimerName);
        }
    }
}
