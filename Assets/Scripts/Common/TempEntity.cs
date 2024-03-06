using UnityEngine;
using UnityEngine.Events;

namespace TD
{
    public class TempEntity : EntityWithTimer
    {
        [SerializeField] private bool showtimer;

        private static string _TTLTimerName = "ttl";

        [SerializeField] private float _TTL;

        private UnityEvent _OnEnd = new UnityEvent();
        public UnityEvent EventOnEnd => _OnEnd;

        private void Awake()
        {
            AddTimer(_TTLTimerName, _TTL);
            AddCallback(_TTLTimerName, () => Dispose());
        }

        private void Dispose()
        {
            _OnEnd.AddListener(() => Destroy(gameObject));
            _OnEnd.Invoke();
        }
    }
}
