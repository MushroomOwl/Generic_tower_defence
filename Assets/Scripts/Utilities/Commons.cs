using UnityEngine;

namespace TD
{
    public static class Utilities
    {
        public enum MouseButton
        {
            Left = 0,
            Right = 1,
            Middle = 2
        }

        public static Vector2 GetMouse2DWorldPosition()
        {
            return GetMouseWorldPosition();
        }

        public static Vector3 GetMouseWorldPosition()
        {
            return GetMouseWorldPositionFromCamera(Camera.main);
        }

        public static Vector3 GetMouseWorldPositionFromCamera(Camera camera)
        {
            return ScreenToWorldPosition(Input.mousePosition, camera);
        }

        public static Vector3 ScreenToWorldPosition(Vector3 screenPosition, Camera camera)
        {
            return camera.ScreenToWorldPoint(screenPosition);
        }

        public static Vector3 WorldToScreenPosition(Vector3 worldPos)
        {
            return WorldToScreenPosition(worldPos, Camera.main);
        }

        public static Vector3 WorldToScreenPosition(Vector3 worldPos, Camera camera)
        {
            return camera.WorldToScreenPoint(worldPos);
        }

        public static void CloneTransform(Transform source, ref Transform dest)
        {
            dest.position = source.position;
            dest.rotation = source.rotation;
            dest.localScale = source.localScale;
        }
    }
}
