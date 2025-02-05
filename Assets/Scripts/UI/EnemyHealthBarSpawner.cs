using UnityEngine;
using UnityEngine.Animations;

namespace Scarcity
{
    public class EnemyHealthBarSpawner : MonoBehaviour
    {
        public Vector3 offset = Vector3.up * 0.3f;

        [SerializeField]
        private HealthBar healthBarPrefab;
        private HealthBar healthBarInstance;

        private Health health;

        private void Awake()
        {
            health = GetComponentInParent<Health>();
        }

        private void OnEnable()
        {
            healthBarInstance = Instantiate(healthBarPrefab, MainCanvas.Instance.transform);
            healthBarInstance.SetTrackedHealth(health);

            PositionConstraintUI constraint = healthBarInstance.GetComponent<PositionConstraintUI>();
            constraint.target = transform;
            constraint.offset = offset;
        }

        private void OnDisable()
        {
            if (healthBarInstance)
                Destroy(healthBarInstance.gameObject);
        }
    }
}
