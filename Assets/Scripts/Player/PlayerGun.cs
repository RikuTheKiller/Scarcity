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
            var projectile = projectilePrefab.pool.Get<Projectile2D>();
            projectile.Fire(rigidbody);
        }
    }
}