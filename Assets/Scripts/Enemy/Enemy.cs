using UnityEngine;

namespace Scarcity
{
    public class Enemy : MonoBehaviour
    {
        public Health health;

        public int baseDamage = 10;

        private void Reset()
        {
            health = GetComponent<Health>();
        }

        private void OnEnable()
        {
            if (!health) return;
            health.Update += OnHealthUpdate;
        }

        private void OnDisable()
        {
            if (!health) return;
            health.Update -= OnHealthUpdate;
        }

        private void OnHealthUpdate(int health)
        {
            if (health <= 0) Destroy(gameObject);
        }
    }
}