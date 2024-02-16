using System;
using System.Collections.Generic;
using UnityEngine;

namespace TD
{
    public class AIControllerTower : EntityWithTimer
    {
        public enum AIState
        {
            Targeting
        }


        [SerializeField] private AIState _State;
        [SerializeField] private Tower _Tower;
        [SerializeField] private float _Radius;
        [SerializeField] private Destructable _Target;

        void Start()
        {
            _Tower = GetComponent<Tower>();
        }

        protected void Update()
        {
            switch (_State)
            {
                case AIState.Targeting:
                    TargetingActions();
                    break;
            }
        }

        void TargetingActions()
        {
            if (_Target != null)
            {
                _Tower.TurnTurretTowards(_Target.transform.position);
                _Tower.Fire(_Target);
                if ((_Target.transform.position - transform.position).magnitude > _Radius)
                {
                    _Target = null;
                }
            }
            else
            {
                FindTarget();
            }
        }

        void FindTarget()
        {
            if (_Target != null)
            {
                return;
            }

            float minSqrDistance = 10000;
            float sqrDetectionRange = _Radius * _Radius;
            foreach (var target in Destructable.AllDestructables)
            {
                float sqrDistance = (transform.position - target.transform.position).sqrMagnitude;

                if (sqrDistance > sqrDetectionRange)
                {
                    continue;
                }

                if (sqrDistance > minSqrDistance)
                {
                    continue;
                }

                minSqrDistance = sqrDistance;
                _Target = target;
            }
        }

#if UNITY_EDITOR

        private void OnDrawGizmos()
        {
            GizmosExtensions.DrawCircle(new Vector3(transform.position.x, transform.position.y, 0), _Radius, 20, Color.green);
        }

#endif
    }
}
