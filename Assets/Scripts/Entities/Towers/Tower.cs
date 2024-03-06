using UnityEditor;
using UnityEngine;

namespace TD
{
    [RequireComponent(typeof(AIControllerTower))]
    public class Tower: MonoBehaviour
    {
        [SerializeField] private TowerProperties _Setup;
        [SerializeField] private SpriteRenderer _VisualModel;
        [SerializeField] private SpriteRenderer _WeaponVisualModel;

        [SerializeField] private Transform _WeaponMountPoint;
        [SerializeField] private Weapon _Weapon;

        public void ApplySetup(TowerProperties setup)
        {
            _Setup = setup;
            _Weapon.AssignLoadout(_Setup.Weapon);
            _WeaponMountPoint.localPosition = new Vector3(_Setup.WeaponMountPoint.x, _Setup.WeaponMountPoint.y, 0);
            _VisualModel.sprite = _Setup.VisualModel;
            GetComponent<AIControllerTower>().SetDetectionRadius(_Setup.Range);
        }

        public void TurnTurretTowards(Vector3 position)
        {
            Vector2 localCoordinatesDest = _WeaponMountPoint.transform.InverseTransformPoint(position);
            float angleToDestination = -Vector3.SignedAngle(localCoordinatesDest, Vector3.up, Vector3.forward);
            _WeaponMountPoint.Rotate(0, 0, angleToDestination);
        }

        public void Fire(Destructable target)
        {
            _Weapon.Fire(target);
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(_WeaponMountPoint.transform.position, _WeaponMountPoint.transform.position + _WeaponMountPoint.transform.up.normalized);
        }

        [CustomEditor(typeof(Tower))]
        public class TowerInspector : Editor
        {
            public override void OnInspectorGUI()
            {
                base.OnInspectorGUI();

                TowerProperties setup = EditorGUILayout.ObjectField(null, typeof(TowerProperties), false) as TowerProperties;

                if (setup != null)
                {
                    (target as Tower).ApplySetup(setup);
                }
            }
        }
#endif
    }
}
