using UnityEngine;

namespace TD
{
    public class AreaCircle2D: Area2D
    {
        [SerializeField] private float _Radius;

        public override Vector2 GetRandomPointInside()
        {
            return (Vector2)transform.position + (Vector2)Random.insideUnitSphere * _Radius;
        }

        public override bool IsPointWithinArea(Vector2 point)
        {
            return (new Vector2(transform.position.x - point.x, transform.position.y - point.y)).magnitude <= _Radius;
        }

#if UNITY_EDITOR
        public override void DrawAreaGizmos()
        {
            GizmosExtensions.DrawCircle(gameObject.transform.position, _Radius, 20, Color.yellow);
        }

        protected void OnDrawGizmos()
        {
            DrawAreaGizmos();
        }
#endif
    }
}

