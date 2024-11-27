using UnityEngine;

namespace Scarcity
{
    public class PlayerGun : MonoBehaviour
    {
        public Rigidbody2D rigidbody;
        public Projectile2D projectilePrefab;

        private void Reset()
        {
            rigidbody = GetComponentInParent<Rigidbody2D>();
        }

        private void OnEnable()
        {
            Input.Attack.Performed += Fire;
        }

        private void OnDisable()
        {
            Input.Attack.Performed -= Fire;
        }

        public void Fire()
        {
            if (!projectilePrefab) return;
            Projectile2D projectile = Instantiate(projectilePrefab);
            projectile.OnHit += OnHit;
            projectile.Fire(transform);
        }

        private void OnHit(Projectile2D projectile, Collision collision)
        {
            Destroy(projectile);
        }
    }
}