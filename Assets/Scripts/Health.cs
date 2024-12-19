using System;
using UnityEngine;

namespace Scarcity
{
    public class Health : MonoBehaviour
    {
        public int health = 100;
        public int maxHealth = 100;

        public event Action<int> Update;

        private void Start()
        {
            Update?.Invoke(health);
        }

        public void SetHealth(int amount)
        {
            var oldHealth = health;

            health = Math.Clamp(amount, 0, maxHealth);

            if (health != oldHealth) Update?.Invoke(health);
        }

        public void TakeDamage(int amount)
        {
            if (amount <= 0) return;
            SetHealth(health - amount);
        }

        public void HealDamage(int amount)
        {
            if (amount <= 0) return;
            SetHealth(health + amount);
        }
    }
}
