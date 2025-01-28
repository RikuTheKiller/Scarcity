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

        private void Reset()
        {
            projectile = GetComponent<Projectile2D>();
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Awake()
        {
            startingScale = transform.localScale;
        }

        private void OnEnable()
        {
            spawnTime = Time.time;
            UpdateState();
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
    }
}
