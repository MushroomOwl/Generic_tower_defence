using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

namespace TD
{
    [RequireComponent(typeof(Collider2D))]
    internal class DotArea : TempEntity
    {
        [SerializeField] private float _TickTime;
        [SerializeField] private int _TickDamage;

        private HashSet<Destructable> _Destructables;
        private const string _TickTimer = "tick_dot_timer";

        private void Start()
        {
            _Destructables = new HashSet<Destructable>();
            AddTimer(_TickTimer, _TickTime);
            AddCallback(_TickTimer, DamageAllEntitiesInside);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            Destructable dest = collision.gameObject.GetComponent<Destructable>();

            if (dest == null)
            {
                return;
            }

            _Destructables.Add(dest);
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            Destructable dest = collision.gameObject.GetComponent<Destructable>();

            if (dest == null)
            {
                return;
            }

            if (_Destructables.Contains(dest))
            {
                _Destructables.Remove(dest);
            }
        }

        private void DamageAllEntitiesInside()
        {
            if (_Destructables.Count == 0)
            {
                return;
            }

            UnityEvent applyDamageToAll = new UnityEvent();

            foreach (var entity in _Destructables)
            {
                applyDamageToAll.AddListener(()=> entity.ApplyDamage(_TickDamage));
            }

            applyDamageToAll.Invoke();
        }

        private void OnDestroy()
        {
            RemoveTimer(_TickTimer);
        }
    }
}
