using UnityEngine;

namespace Scarcity
{
    public class HealthBar : MonoBehaviour
    {
        public Health trackedHealth;
        public Transform fill;

        private void OnEnable()
        {
            if (!fill || !trackedHealth) return;
            trackedHealth.Update += OnHealthUpdate;
        }

        private void OnDisable()
        {
            if (!fill || !trackedHealth) return;
            trackedHealth.Update -= OnHealthUpdate;
        }

        private void OnHealthUpdate(int health)
        {
            fill.transform.localScale = new(health / (float)trackedHealth.maxHealth, 1, 1);
        }
    }
}
