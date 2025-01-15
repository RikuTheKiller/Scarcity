using UnityEngine;

namespace Scarcity
{
    public class PlayerGun : MonoBehaviour
    {
        public Rigidbody2D rigidbody;
        public Projectile2D projectilePrefab;
        public LayerMask targetLayers;

        public float firingCooldown = 1;
        private float nextFiringTime;

        private void Reset()
        {
            rigidbody = GetComponentInParent<Rigidbody2D>();
        }

        private void Update()
        {
            if (Input.Attack.Pressed)
            {
                TryFire();
            }
        }

        public void TryFire()
        {
            if (nextFiringTime > Time.time) return;
            nextFiringTime = Time.time + firingCooldown;

            Fire();
        }

        public void Fire()
        {
            if (!projectilePrefab) return;

            var projectile = projectilePrefab.pool.Get<Projectile2D>();

            projectile.LayerMask = targetLayers;
            projectile.Fire(transform, rigidbody.linearVelocity);
        }
    }
}