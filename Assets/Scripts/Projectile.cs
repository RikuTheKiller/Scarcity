using System;
using System.Collections;
using UnityEngine;

namespace Scarcity
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Projectile2D : MonoBehaviour
    {
        public Rigidbody2D rigidbody;

        public float speed = 10;
        public int damage = 0;
        public float lifetime = 10;
        public LayerMask layerMask;

        [HideInInspector]
        public float endTime = 0;

        public event Action<Projectile2D, Collision> OnHit;

        protected virtual void Reset()
        {
            rigidbody = GetComponent<Rigidbody2D>();
        }

        protected virtual void Update()
        {
            if (Time.time >= endTime) gameObject.SetActive(false);
        }

        public virtual void Fire()
        {
            gameObject.SetActive(true);
            rigidbody.linearVelocity = transform.up * speed;
            endTime = Time.time + lifetime;
        }

        public virtual void Fire(Vector2 position, Vector2 direction)
        {
            transform.SetPositionAndRotation(position, Quaternion.LookRotation(Vector3.forward, direction));
            Fire();
        }

        public virtual void Fire(Transform from)
        {
            Fire(from.position, from.up);
        }

        public virtual void Fire(Rigidbody2D from)
        {
            Fire(from.transform);
            rigidbody.linearVelocity += from.linearVelocity;
        }

        public virtual bool CanHit(Collision collision) => (collision.gameObject.layer & layerMask) != 0;

        public virtual void Hit(Collision collision)
        {
            if (collision.gameObject.TryGetComponent(out Health health))
            {
                health.TakeDamage(damage);
            }

            gameObject.SetActive(false);

            OnHit?.Invoke(this, collision);
        }

        protected virtual void OnCollisionEnter(Collision collision)
        {
            if (CanHit(collision)) Hit(collision);
        }
    }
}
