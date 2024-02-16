using UnityEngine;

namespace TD
{
    public class Projectile : TempEntity
    {
        [SerializeField] private float _Velocity;
        [SerializeField] private float _AngularVelocityDeg;
        [SerializeField] private int _Damage;
        [SerializeField] private Destructable _Source;
        [SerializeField] private bool _HasCollider;

        public Destructable Source => _Source;

        void Start()
        {
            Collider2D collider = GetComponent<Collider2D>();
            if (collider != null)
            {
                _HasCollider = true;
            }
        }

        public void ApplySetup(WeaponProperties props)
        {
            _Velocity = props.PVelocity;
            _Damage = props.PDamage;
        }

        private void Update()
        {
            float stepLength = Time.deltaTime * _Velocity;

            if (_AngularVelocityDeg != 0)
            {
                float angleDeg = _AngularVelocityDeg * Time.deltaTime;
                transform.rotation = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z + angleDeg);
            }

            Vector2 step = transform.up * stepLength;

            if (_HasCollider)
            {
                transform.position += new Vector3(step.x, step.y, 0);
                return;
            }

            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, stepLength);

            if (hit)
            {
                Destructable dest = hit.collider.GetComponentInParent<Destructable>();

                if (dest == null)
                {
                    transform.position += new Vector3(step.x, step.y, 0);
                    return;
                }

                if (dest != null)
                {
                    //if (dest.FractionId == _Source.FractionId)
                    //{
                    //    transform.position += new Vector3(step.x, step.y, 0);
                    //    return;
                    //}

                    dest.ApplyDamage(_Damage);
                }

                transform.position += new Vector3(hit.point.x - transform.position.x, hit.point.y - transform.position.y, 0);
                OnProjectileImpact(hit.collider, hit.point);

                return;
            }
            else
            {
                transform.position += new Vector3(step.x, step.y, 0);
            }
        }

        public void SetAngularVelocityDeg(float vel)
        {
            _AngularVelocityDeg = vel;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            Destructable dest = collision.GetComponentInParent<Destructable>();

            if (dest == null)
            {
                return;
            }

            if (dest != null)
            {
                //if (dest.FractionId == _Source.FractionId) return;

                dest.ApplyDamage(_Damage);
            }

            OnProjectileImpact(collision, transform.position);
        }

        private void OnProjectileImpact(Collider2D col, Vector2 pos)
        {
            Destroy(gameObject);
        }

        public void SetSource(Destructable parent)
        {
            _Source = parent;
        }
    }
}
