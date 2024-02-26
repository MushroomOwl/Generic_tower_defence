using UnityEngine;

namespace TD
{
    public enum ProjectileTrajectory {
        StrictFollow,
        StraightLine,
        StraightLineGround
    }

    [CreateAssetMenu(fileName = "weapon_")]
    public sealed class WeaponProperties : ScriptableObject
    {
        [Header("Weapon properties")]
        
        [SerializeField] private float _ShotsPerSecond;
        public float ShotsPerSecond => _ShotsPerSecond;

        [SerializeField] private Sprite _VisualModel;
        public Sprite VisualModel => _VisualModel;

        [Header("Projectile visual model")]

        [SerializeField] private Projectile _ProjectilePrefab;
        public Projectile ProjectilePrefab => _ProjectilePrefab;

        [Header("Projectile properties")]

        [SerializeField] private int _PDamage;
        public int PDamage => _PDamage;

        [SerializeField] private int _PVelocity;
        public int PVelocity => _PVelocity;

        [SerializeField] private ProjectileTrajectory _PTrajectory;
        public ProjectileTrajectory PTrajectory => _PTrajectory;
    }
}
