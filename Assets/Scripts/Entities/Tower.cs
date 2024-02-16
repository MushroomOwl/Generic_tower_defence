using UnityEngine;

namespace TD
{
    internal class Tower: MonoBehaviour
    {
        [SerializeField] private Transform _TurretPoint;
        [SerializeField] private Weapon _TurretWeapon;

        public void TurnTurretTowards(Vector3 position)
        {
            Vector2 localCoordinatesDest = _TurretPoint.transform.InverseTransformPoint(position);
            float angleToDestination = -Vector3.SignedAngle(localCoordinatesDest, Vector3.up, Vector3.forward);
            _TurretPoint.Rotate(0, 0, angleToDestination);
        }

        public void Fire()
        {
            _TurretWeapon.Fire();
        }

        public void Fire(Destructable target)
        {
            _TurretWeapon.Fire(target);
        }

#if UNITY_EDITOR

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(_TurretPoint.transform.position, _TurretPoint.transform.position + _TurretPoint.transform.up.normalized);
        }

#endif
    }
}
