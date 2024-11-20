using System.Collections;
using UnityEngine;

namespace Scarcity
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Projectile2D : MonoBehaviour
    {
        public Rigidbody2D rigidbody;

        public float speed = 10;
        public float damage = 0;
        public float lifetime = 10;

        [HideInInspector]
        public float endTime = 0;

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

        public virtual bool CanHit(Collision collision) => true;

        public virtual void OnHit(Collision collision)
        {
            gameObject.SetActive(false);
        }

        protected virtual void OnCollisionEnter(Collision collision)
        {
            if (!CanHit(collision)) OnHit(collision);
        }
    }
}
