using UnityEngine;

namespace Scarcity
{
    public class Gun : MonoBehaviour
    {
        public Transform pivot;
        public Rigidbody2D rigidbody;
        public Projectile2D projectilePrefab;
        public LayerMask targetLayers;

        [Space]

        public AudioSource audioSource;
        public AudioClip firingSound;
        public float firingSoundVolume = 1;

        [Space]

        public float firingCooldown = 1;
        private float nextFiringTime;

        private void Reset()
        {
            pivot = transform;
            rigidbody = GetComponentInParent<Rigidbody2D>();
            audioSource = GetComponent<AudioSource>();
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

            if (rigidbody)
            {
                projectile.Fire(transform, rigidbody.linearVelocity);
            }
            else
            {
                projectile.Fire(transform);
            }

            if (firingSound && audioSource)
            {
                audioSource.PlayOneShot(firingSound, firingSoundVolume);
            }
        }
    }
}