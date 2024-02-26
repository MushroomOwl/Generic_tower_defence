using UnityEngine;

namespace TD
{
    [CreateAssetMenu(fileName = "tower_")]
    public class TowerProperties : ScriptableObject
    {
        [SerializeField] private Sprite _VisualModel;
        public Sprite VisualModel => _VisualModel;

        [SerializeField] private Vector2 _WeaponMountPoint;
        public Vector2 WeaponMountPoint => _WeaponMountPoint;

        [SerializeField] private WeaponProperties _Weapon;
        public WeaponProperties Weapon => _Weapon;

        [SerializeField] private float _Range;
        public float Range => _Range;

        [SerializeField] private int _Cost;
        public int Cost => _Cost;

#nullable enable
        [SerializeField] private TowerProperties? _NextLevelTower;
        public TowerProperties? NextLevelTower => _NextLevelTower;
#nullable disable
    }
}
