using UnityEngine;

namespace TD
{
    [CreateAssetMenu(fileName = "weapon_")]
    public sealed class WeaponProperties : ScriptableObject
    {
        [Header("Weapon properties")]
        
        [SerializeField] private float _ShotsPerSecond;
        public float ShotsPerSecond => _ShotsPerSecond;

        [SerializeField] private Sprite _VisualModel;
        public Sprite VisualModel => _VisualModel;

        [Header("Projectile visual model")]

        [SerializeField] private Sprite _ProjectileVisualModel;
        public Sprite ProjectileVisualModel => _ProjectileVisualModel;

        [Header("Projectile properties")]

        [SerializeField] private int _PDamage;
        public int PDamage => _PDamage;

        [SerializeField] private int _PVelocity;
        public int PVelocity => _PVelocity;

        [SerializeField] private int _PAngVelocity;
        public int PAngVelocity => _PAngVelocity;

        [SerializeField] private GameObject _PImpactEffect;
        public GameObject PImpactEffect => _PImpactEffect;

        [SerializeField] private ProjectileTrajectory _PTrajectory;
        public ProjectileTrajectory PTrajectory => _PTrajectory;
    }
}
