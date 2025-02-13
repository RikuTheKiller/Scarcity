using System;
using System.Collections.Generic;
using UnityEngine;

namespace Scarcity
{
    public class Enemy : MonoBehaviour
    {
        public static List<Enemy> All = new();
        public static Action<int> EnemyCountChanged;

        public Health health;
        public NavigationUser navigationUser;

        public int baseDamage = 10;
        public int killReward = 10;

        private void Reset()
        {
            health = GetComponent<Health>();
            navigationUser = GetComponent<NavigationUser>();
        }

        private void OnEnable()
        {
            if (!health) return;
            health.Update += OnHealthUpdate;
            All.Add(this);
            EnemyCountChanged?.Invoke(All.Count);
        }

        private void OnDisable()
        {
            if (!health) return;
            health.Update -= OnHealthUpdate;
            All.Remove(this);
            EnemyCountChanged?.Invoke(All.Count);
        }

        private void OnHealthUpdate(int health)
        {
            if (health > 0) return;

            Destroy(gameObject);
            Money.Adjust(killReward);

            if (All.Count == 0 && WaveSpawner.lastWaveOver)
            {
                StuffRefs.Instance.GameOver.SetActive(true);
                Input.Actions.Disable();
            }
        }
    }
}