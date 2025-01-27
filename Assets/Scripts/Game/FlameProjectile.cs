using UnityEngine;

namespace Scarcity
{
    public class FlameProjectile : MonoBehaviour
    {
        public float rotationSpeed = 1;
        public float scaleSpeed = 1;

        private float spawnTime = 0;

        private void OnEnable()
        {
            spawnTime = Time.time;
        }

        private void Update()
        {
            float timeSinceSpawn = Time.time - spawnTime;

            transform.rotation = Quaternion.Euler(0, 0, timeSinceSpawn * rotationSpeed * 360);
            transform.localScale = Vector3.one * (1 + timeSinceSpawn * scaleSpeed);
        }
    }
}
