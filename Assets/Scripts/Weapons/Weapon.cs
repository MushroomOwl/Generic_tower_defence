using UnityEngine;

namespace TD
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] private WeaponProperties _WeaponProperties;
        [SerializeField] private SpriteRenderer _VisualModel;

        public WeaponProperties Loadout => _WeaponProperties;

        private float _ReloadTimer = 0;

        public bool CanFire => _ReloadTimer <= 0;

        private void Update()
        {
            if (_ReloadTimer > 0)
            {
                _ReloadTimer -= Time.deltaTime;
            }
        }

        public void Fire()
        {
            if (_WeaponProperties == null) return;

            if (!CanFire) return;

            if (!_WeaponProperties.ProjectilePrefab) return;

            Projectile projectile = Instantiate(_WeaponProperties.ProjectilePrefab).GetComponent<Projectile>();
            projectile.transform.position = transform.position;

            _ReloadTimer = _WeaponProperties.ShotsPerSecond == 0 ? 1 : 1 /_WeaponProperties.ShotsPerSecond;
        }

        public void Fire(Destructable target)
        {
            if (_WeaponProperties == null) return;

            if (!CanFire) return;

            if (!_WeaponProperties.ProjectilePrefab) return;

            Projectile projectile = Instantiate(_WeaponProperties.ProjectilePrefab).GetComponent<Projectile>();
            projectile.transform.position = transform.position;
            projectile.transform.rotation = transform.rotation;
            projectile.ApplySetup(_WeaponProperties);
            
            _ReloadTimer = _WeaponProperties.ShotsPerSecond == 0 ? 1 : 1 / _WeaponProperties.ShotsPerSecond;
        }

        public void AssignLoadout(WeaponProperties props)
        {
            _WeaponProperties = props;
            _VisualModel.sprite = _WeaponProperties.VisualModel;
            _ReloadTimer = 0;
        }
    }
}
