using UnityEditor;
using UnityEngine;

namespace TD
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Enemy: Destructable
    {
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
            _Animator.speed = _Setup.AnimSpeed;
            
            ChangeMaxHP(_Setup.MaxHP);
            transform.localScale = new Vector3(_Setup.Size, _Setup.Size, _Setup.Size);
        }

        public void MakeAStep(Vector2 direction)
        {
            if (!_Initialized)
            {
                return;
            }

            Vector2 step = direction.normalized * _Setup.MovementSpeed * Time.deltaTime;
            transform.position = new Vector3(transform.position.x + step.x, transform.position.y + step.y);

            _Renderer.flipX = step.x < 0;
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
