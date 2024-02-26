using UnityEngine;

namespace TD
{
    public abstract class Area2D: MonoBehaviour
    {
        public abstract Vector2 GetRandomPointInside();
        public abstract bool IsPointWithinArea(Vector2 point);

#if UNITY_EDITOR 
        public abstract void DrawAreaGizmos();
#endif
    }
}
