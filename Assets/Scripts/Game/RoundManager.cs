using System;
using UnityEngine;

namespace Scarcity
{
    public class RoundManager : MonoBehaviour
    {
        public static RoundManager Instance { get; private set; }

        public Health baseHealth;

        public GameObject gameOverScreen;

        public NavigationNode[] exitNodes;

        public static event Action GameOver;

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
            baseHealth.Update += OnHealthUpdate;

            foreach (var node in exitNodes)
            {
                node.Arrived += OnExit;
            }
        }

        private void OnDisable()
        {
            baseHealth.Update -= OnHealthUpdate;

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

        private void OnHealthUpdate(int health)
        {
            if (health > 0) return;

            gameOverScreen.SetActive(true);
            Time.timeScale = 0;

            GameOver?.Invoke();
            Input.Actions.Disable();
        }
    }
}
