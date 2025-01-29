using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scarcity
{
    public class Projectile2D : MonoBehaviour
    {
        public Rigidbody2D rigidbody;
        public ObjectPool pool;

        public float speed = 10;
        public int damage = 0;
        public float lifetime = 10;

        public bool piercing = false;

        public LayerMask LayerMask { get => rigidbody.includeLayers; set => rigidbody.includeLayers = value; }

        private float endTime = 0;
        private List<Rigidbody2D> piercedTargets = new();

        public event Action<Collider2D> OnHit;

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
            if (Time.time >= endTime) pool.Release(this);
        }

        public void Fire()
        {
            rigidbody.linearVelocity = transform.up * speed;
            endTime = Time.time + lifetime;
            piercedTargets.Clear();
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

        public void Fire(Transform from, Vector2 baseVelocity)
        {
            Fire(from);
            rigidbody.linearVelocity += baseVelocity;
        }

        public void Hit(Collider2D collider)
        {
            if (piercing && piercedTargets.Contains(collider.attachedRigidbody))
                return;

            if (collider.attachedRigidbody && collider.attachedRigidbody.TryGetComponent(out Health health))
            {
                health.TakeDamage(damage);
            }

            OnHit?.Invoke(collider);

            if (!piercing)
            {
                pool.Release(this);
            }
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            Hit(collider);
        }
    }
}
