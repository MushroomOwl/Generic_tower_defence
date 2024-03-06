using UnityEngine;

namespace TD
{
    public enum ProjectileTrajectory
    {
        StrictFollow,
        StraightLine,
        StraightLineGround
    }

    public class Projectile : TempEntity, IPoolable<Projectile>, ICustomPrototype<Projectile>
    {
        [SerializeField] private ProjectilePool _projectilePool;

        [SerializeField] private float _Velocity;
        [SerializeField] private float _AngularVelocityDeg;
        [SerializeField] private int _Damage;

        [SerializeField] private Destructable _Target;
        [SerializeField] private Vector3 _TargetPositionOnFire;
        [SerializeField] private ProjectileTrajectory _Trajectory;
        
        [SerializeField] private SpriteRenderer _VisualModel;

        [SerializeField] private GameObject _ImpactEffect;

        public void ApplySetup(WeaponProperties props)
        {
            _Velocity = props.PVelocity;
            _AngularVelocityDeg = props.PAngVelocity;
            _Damage = props.PDamage;
            _VisualModel.sprite = props.ProjectileVisualModel;
            _Trajectory = props.PTrajectory;
            _ImpactEffect = props.PImpactEffect;
        }

        public void SetTarget(Destructable target)
        {
            _Target = target;
            _TargetPositionOnFire = target.gameObject.transform.position;
        }

        private void OnEnable()
        {
            ResetTTL();
        }

        private void Update()
        {
            Vector2 step = GetNextStep();

            (bool isHit, Destructable dest, Vector3 hitPoint) = CheckOnHit(step.magnitude);

            if (!isHit)
            {
                transform.position += new Vector3(step.x, step.y, 0);
                return;
            }

            OnHit(dest, hitPoint);
        }

        // TODO: function like this shouldn't change rotation itself
        private Vector2 GetNextStep()
        {
            float stepLength = Time.deltaTime * _Velocity;

            if (_Target == null)
            {
                _Trajectory = ProjectileTrajectory.StraightLine;
            }

            if (_Trajectory == ProjectileTrajectory.StrictFollow)
            {
                Vector2 direction = _Target.transform.position - transform.position;
                direction.Normalize();
                float rotationDeg = Mathf.Asin(Vector3.Cross(transform.up, direction).z) * Mathf.Rad2Deg;
                float angleDeg = Utilities.MathfClampAbs(_AngularVelocityDeg * Time.deltaTime, Mathf.Abs(rotationDeg));
                transform.rotation = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z + angleDeg);
            }

            return transform.up * stepLength;
        }

        private (bool, Destructable, Vector3) CheckOnHit(float stepLength)
        {
            switch (_Trajectory)
            {
                case ProjectileTrajectory.StraightLine:
                case ProjectileTrajectory.StrictFollow:
                    return CheckDirectHit(stepLength);
                case ProjectileTrajectory.StraightLineGround:
                    return CheckPositionHit(stepLength);
                default:
                    return CheckDirectHit(stepLength);
            }
        }

        private (bool, Destructable, Vector3) CheckPositionHit(float stepLength)
        {
            Vector3 distance = gameObject.transform.position - _TargetPositionOnFire;

            if (distance.magnitude > stepLength)
            {
                return (false, null, Vector3.zero);
            }

            return (true, null, _TargetPositionOnFire);
        }

        private (bool, Destructable, Vector3) CheckDirectHit(float stepLength)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, stepLength);

            if (!hit)
            {
                return (false, null, Vector3.zero);
            }

            Destructable dest = hit.collider.GetComponentInParent<Destructable>();

            if (!dest)
            {
                return (false, null, Vector3.zero);
            }

            return (true, dest, hit.point);
        }

        private void OnHit(Destructable dest, Vector3 hitPoint)
        {
            if (dest != null)
            {
                dest.ApplyDamage(_Damage);
            }

            transform.position += new Vector3(hitPoint.x - transform.position.x, hitPoint.y - transform.position.y, 0);
            OnProjectileImpact();
        }

        private void OnProjectileImpact()
        {
            if (_ImpactEffect != null )
            {
                Instantiate(_ImpactEffect, gameObject.transform.position, Quaternion.identity);
            }

            DestroySelf();
        }

        public void OnGetFromPool()
        {
            gameObject.SetActive(true);
        }

        public void OnReleaseToPool()
        {
            gameObject.SetActive(false);
        }

        public void OnDestroyInPool()
        {
            Destroy(gameObject);
        }

        public Projectile CloneSelf()
        {
            return _projectilePool.PoolStantiate();
        }

        public void DestroySelf()
        {
            _projectilePool.PoolStroy(this);
        }
    }
}
