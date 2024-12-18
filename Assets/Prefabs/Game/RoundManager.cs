using UnityEngine;

namespace Scarcity
{
    public class RoundManager : MonoBehaviour
    {
        public static RoundManager Instance { get; private set; }

        public Health baseHealth;

        public NavigationNode[] exitNodes;

        private void Reset()
        {
            baseHealth = GetComponent<Health>();
        }

        private void Awake()
        {
            Instance = this;
        }

        private void OnEnable()
        {
            foreach (var node in exitNodes)
            {
                node.Arrived += OnExit;
            }
        }

        private void OnDisable()
        {
            foreach (var node in exitNodes)
            {
                node.Arrived -= OnExit;
            }
        }

        private void OnExit(GameObject target)
        {
            if (!target.TryGetComponent(out Enemy enemy)) return;

            baseHealth.TakeDamage(enemy.baseDamage);
            Destroy(target);
        }
    }
}
