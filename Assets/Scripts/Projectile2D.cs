using System;
using System.Collections;
using UnityEngine;

namespace Scarcity
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Projectile2D : MonoBehaviour
    {
        public Rigidbody2D rigidbody;
        public ObjectPool pool;

        public float speed = 10;
        public int damage = 0;
        public float lifetime = 10;
        public LayerMask layerMask;

        [HideInInspector]
        public float endTime = 0;

        private void Reset()
        {
            rigidbody = GetComponent<Rigidbody2D>();
        }

        private void OnValidate()
        {
            if (!pool || pool.prefab == this) return;
            pool.prefab = this;
        }

        private void Update()
        {
            if (Time.time >= endTime) gameObject.SetActive(false);
        }

        private void OnDisable()
        {
            pool.Release(this);
        }

        public void Fire()
        {
            gameObject.SetActive(true);
            rigidbody.linearVelocity = transform.up * speed;
            endTime = Time.time + lifetime;
        }

        public void Fire(Vector2 position, Vector2 direction)
        {
            transform.SetPositionAndRotation(position, Quaternion.LookRotation(Vector3.forward, direction));
            Fire();
        }

        public void Fire(Transform from)
        {
            Fire(from.position, from.up);
        }

        public void Fire(Rigidbody2D from)
        {
            Fire(from.transform);
            rigidbody.linearVelocity += from.linearVelocity;
        }

        public bool CanHit(Collision2D collision) => ((1 << collision.gameObject.layer) & layerMask) != 0;

        public void Hit(Collision2D collision)
        {
            if (collision.gameObject.TryGetComponent(out Health health))
            {
                health.TakeDamage(damage);
            }

            gameObject.SetActive(false);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (CanHit(collision)) Hit(collision);
        }
    }
}
