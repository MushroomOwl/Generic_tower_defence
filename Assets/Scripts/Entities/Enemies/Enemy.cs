using UnityEditor;
using UnityEngine;

namespace TD
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Enemy: Destructable, IPoolable<Enemy>, ICustomPrototype<Enemy>
    {
        [SerializeField] private EnemyPool _enemyPool;

        [SerializeField] private GameEvent _OnEnemyKilled;
        [SerializeField] private GameEvent _OnEnemyReachedGoal;

        // + animation_parameters +
        private const string _animVarFloatVSpeed = "VSpeed";
        private const string _animVarBoolVGHSpeed = "VSpeedGHSpeed";
        // - animation_parameters - 

        [SerializeField] private EnemyProps _Setup;
        [SerializeField] private SpriteRenderer _Renderer;
        [SerializeField] private Animator _Animator;
        [SerializeField] private bool _Initialized;

        public void ApplySetup(EnemyProps setup)
        {
            _Setup = setup;
            _Initialized = true;

            _Renderer.sprite = _Setup.Sprite;
            _Animator.runtimeAnimatorController = _Setup.AnimController;
            
            ChangeMaxHP(_Setup.MaxHP);
            transform.localScale = new Vector3(_Setup.Size, _Setup.Size, _Setup.Size);
        }

        public void EnemyKilled()
        {
            _OnEnemyKilled.Raise(this, _Setup);
            DestroySelf();
        }

        public void MakeAStep(Vector2 direction)
        {
            if (!_Initialized)
            {
                return;
            }


            Vector2 step = direction.normalized * _Setup.MovementSpeed * Time.deltaTime;
            transform.position = new Vector3(transform.position.x + step.x, transform.position.y + step.y);

            _Animator.SetFloat(_animVarFloatVSpeed, direction.normalized.y);
            _Animator.SetBool(_animVarBoolVGHSpeed, Mathf.Abs(direction.y) > Mathf.Abs(direction.x)); 

            _Renderer.flipX = _Setup.SpriteFlipped ? step.x > 0 : step.x < 0;
        }

        public void GoalReached()
        {
            _OnEnemyReachedGoal?.Raise(this, null);
        }

        public void OnGetFromPool()
        {
            gameObject.SetActive(true);
        }

        public void OnReleaseToPool()
        {
            gameObject.SetActive(false);
        }

        public void OnDestroyInPool()
        {
            DestroySelf();
        }

        public Enemy CloneSelf()
        {
            return _enemyPool.PoolStantiate();
        }

        public void DestroySelf()
        {
            _enemyPool.PoolStroy(this);
        }
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(Enemy))]
    public class EnemyInspector: Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            
            EnemyProps setup = EditorGUILayout.ObjectField(null, typeof(EnemyProps), false) as EnemyProps;
            
            if (setup != null)
            {
                (target as Enemy).ApplySetup(setup);
            } 
        }
    }
#endif
}
