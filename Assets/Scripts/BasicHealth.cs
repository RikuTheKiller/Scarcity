using System;
using UnityEngine;

namespace Scarcity
{
    public class BasicHealth : MonoBehaviour
    {
        public int health = 100;
        public int maxHealth = 100;

        public void SetHealth(int amount)
        {
            health = Math.Clamp(health + amount, 0, maxHealth);
            if (health <= 0) Destroy(gameObject);
        }

        public void TakeDamage(int amount)
        {
            if (amount <= 0) return;
            health -= amount;
        }

        public void HealDamage(int amount)
        {
            if (amount <= 0) return;
            health += amount;
        }
    }
}
