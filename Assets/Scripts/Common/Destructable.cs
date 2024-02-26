using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Events;

namespace TD
{
    public class Destructable : MonoBehaviour
    {
        private static HashSet<Destructable> _AllDestructables;
        public static IReadOnlyCollection<Destructable> AllDestructables
        {
            get { return _AllDestructables ?? (_AllDestructables = new HashSet<Destructable>()); }
        }

        [SerializeField] protected bool _Indestructable;
        public bool IsIndestructable => _Indestructable;

        [SerializeField] protected int _MaxHitPoints;
        protected int _CurrentHitPoints;

        public int MaxHitPoints => _MaxHitPoints;
        public int CurrentHitPoints => _CurrentHitPoints;

        protected UnityEvent _OnHPZero = new UnityEvent();
        protected UnityEvent _OnHPChange = new UnityEvent();
        protected UnityEvent _OnDestruction = new UnityEvent();

        public UnityEvent OnHPZero => _OnHPZero;
        public UnityEvent OnHPChange => _OnHPChange;
        public UnityEvent OnDestruction => _OnDestruction;

        [SerializeField] private int _FractionId;
        public int FractionId => _FractionId;

        [SerializeField] private int _ScoreOnDestroy;
        public int ScoreOnDestroy => _ScoreOnDestroy;

        private void Awake()
        {
            _OnHPZero.AddListener(() => Destroy(gameObject));
            _OnDestruction.AddListener(() => _AllDestructables.Remove(this));

            if (_AllDestructables == null)
            {
                _AllDestructables = new HashSet<Destructable>();
            }
            _AllDestructables.Add(this);
        }

        private void OnDestroy()
        {
            _OnDestruction?.Invoke();
        }

        protected virtual void Start()
        {
            _CurrentHitPoints = _MaxHitPoints;
            _OnHPChange?.Invoke();
        }

        protected void ChangeMaxHP(int value)
        {
            _MaxHitPoints = value;
            _CurrentHitPoints = value;
        }

        public void ApplyDamage(int damage)
        {
            if (_Indestructable)
            {
                return;
            }

            _CurrentHitPoints -= damage;
            _OnHPChange?.Invoke();

            if (_CurrentHitPoints <= 0)
            {
                _OnHPZero?.Invoke();
            }
        }

        public void RestoreHealth(int heal)
        {
            if (_Indestructable) return;

            _CurrentHitPoints += heal;
            if (_CurrentHitPoints > _MaxHitPoints) _CurrentHitPoints = _MaxHitPoints;

            _OnHPChange?.Invoke();
        }

        public void MakeIndestructable()
        {
            _Indestructable = true;
        }

        public void MakeDestructable()
        {
            _Indestructable = false;
        }
    }
}