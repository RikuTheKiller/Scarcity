using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

namespace Scarcity
{
    public class Enemy : MonoBehaviour
    {
        public static List<Enemy> All = new();

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
        }

        private void OnDisable()
        {
            if (!health) return;
            health.Update -= OnHealthUpdate;
            All.Remove(this);
        }

        private void OnHealthUpdate(int health)
        {
            if (health > 0) return;

            Destroy(gameObject);
            Money.Adjust(killReward);
        }
    }
}