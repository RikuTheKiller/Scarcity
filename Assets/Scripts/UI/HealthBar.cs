using UnityEngine;
using UnityEngine.UI;

namespace Scarcity
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField]
        private Health trackedHealth;
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

        public void SetTrackedHealth(Health health)
        {
            if (!gameObject.activeInHierarchy)
            {
                trackedHealth = health;
                return;
            }

            if (trackedHealth)
            {
                trackedHealth.Update -= OnHealthUpdate;
            }

            trackedHealth = health;
            trackedHealth.Update += OnHealthUpdate;
        }

        private void OnHealthUpdate(int health)
        {
            fill.transform.localScale = new(health / (float)trackedHealth.maxHealth, 1, 1);
        }
    }
}
