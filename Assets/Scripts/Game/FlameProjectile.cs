using Unity.VisualScripting;
using UnityEngine;

namespace Scarcity
{
    public class FlameProjectile : MonoBehaviour
    {
        public Projectile2D projectile;
        public SpriteRenderer spriteRenderer;

        public Gradient gradient;
        public float rotationSpeed = 1;
        public float scaleSpeed = 1;

        private float spawnTime = 0;
        private Vector3 startingScale = Vector3.one;

        private LayerMask enemyLayer;

        private void Reset()
        {
            projectile = GetComponent<Projectile2D>();
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Awake()
        {
            enemyLayer = LayerMask.NameToLayer("Enemy");
            startingScale = transform.localScale;
        }

        private void OnEnable()
        {
            projectile.OnHit += OnHit;
            spawnTime = Time.time;
            UpdateState();
        }

        private void OnDisable()
        {
            projectile.OnHit -= OnHit;
        }

        private void Update()
        {
            UpdateState();
        }

        private void UpdateState()
        {
            float timeSinceSpawn = Time.time - spawnTime;
            spriteRenderer.color = gradient.Evaluate(timeSinceSpawn / projectile.lifetime);
            transform.rotation = Quaternion.Euler(0, 0, timeSinceSpawn * rotationSpeed * 360);
            transform.localScale = startingScale * (1 + timeSinceSpawn * scaleSpeed);
        }

        private void OnHit(Collider2D collider)
        {
            if (collider.attachedRigidbody.gameObject.layer == enemyLayer)
            {
                collider.attachedRigidbody.GetOrAddComponent<FireStatusEffect>().ResetTime();
            }
        }
    }
}
