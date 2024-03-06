using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace TD
{
    public class Destructable : MonoBehaviour
    {
        private static HashSet<Destructable> _allDestructables = new HashSet<Destructable>();
        public static IReadOnlyCollection<Destructable> AllDestructables
        {
            get { return _allDestructables ?? (_allDestructables = new HashSet<Destructable>()); }
        }

        [SerializeField] private UnityEvent _OnEnemyZeroHP;

        [SerializeField] protected int _MaxHitPoints;
        public int MaxHitPoints => _MaxHitPoints;

        [SerializeField] protected int _CurrentHitPoints;
        public int CurrentHitPoints => _CurrentHitPoints;

        protected virtual void Start()
        {
            _CurrentHitPoints = _MaxHitPoints;
        }

        protected void ChangeMaxHP(int value)
        {
            _MaxHitPoints = value;
            _CurrentHitPoints = value;
        }

        public void ApplyDamage(int damage)
        {
            _CurrentHitPoints -= damage;

            if (_CurrentHitPoints <= 0)
            {
                _OnEnemyZeroHP?.Invoke();
            }
        }

        public void RestoreHealth(int heal)
        {
            _CurrentHitPoints += heal;
            if (_CurrentHitPoints > _MaxHitPoints) _CurrentHitPoints = _MaxHitPoints;
        }

        private void OnEnable()
        {
            _allDestructables.Add(this);
        }

        private void OnDisable()
        {
            _allDestructables.Remove(this);
        }
    }
}