using UnityEngine;

namespace TD
{
    public class AIControllerEnemy : EntityWithTimer
    {
        public enum AIState
        {
            PatrolPath
        }

        [SerializeField] private Enemy _Enemy;

        [SerializeField] private AIState _State;

        [SerializeField] private Vector2 _Destination;

        [SerializeField] private AreaPath2D _PatrolPath;
        [SerializeField] private int _CurrentPoint = 0;

        void Start()
        {
            _Enemy = GetComponent<Enemy>();
        }

        public void SetPath(AreaPath2D path)
        {
            _CurrentPoint = 0;
            _PatrolPath = path;
            _Destination = Vector2.zero;
        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();

            switch (_State)
            {
                case AIState.PatrolPath:
                    PatrolPathActions();
                    break;
            }
        }

        void PatrolPathActions()
        {
            if (_PatrolPath.Length == 0)
            {
                return;
            }

            PatrolPathCalcCourse();
        }

        void PatrolPathCalcCourse()
        {
            if (_Destination == null || _Destination == Vector2.zero)
            {
                _Destination = _PatrolPath[_CurrentPoint].GetRandomPointInside();
            }

            Vector2 distToPosition = new Vector2(_Destination.x - transform.position.x, _Destination.y - transform.position.y);

            if (distToPosition.magnitude < 0.1f)
            {
                if (_CurrentPoint >= _PatrolPath.Length - 1)
                {
                    _Enemy.GoalReached();
                    _Enemy.DestroySelf();
                    return;
                } else
                {
                    _CurrentPoint++;
                }
                _Destination = _PatrolPath[_CurrentPoint].GetRandomPointInside();
            }

            _Enemy.MakeAStep(distToPosition.normalized);
        }
    }
}
