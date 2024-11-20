using System;
using UnityEngine;

namespace Scarcity
{
    public class HealthHandler : MonoBehaviour
    {
        public virtual float Health {
            get => _health;

            set
            {
                float oldHealth = Health;
                _health = Mathf.Clamp(value, 0, MaxHealth);

                float health = Health;

                if (oldHealth != health)
                {
                    OnHealthChanged?.Invoke(this, oldHealth, health);
                }

                if (health <= 0) OnDeath();
            }
        }

        public virtual float MaxHealth
        {
            get => _maxHealth;

            set
            {
                _maxHealth = Mathf.Abs(value);
                Health = Health;
            }
        }

        [SerializeField] private float _health;
        [SerializeField] private float _maxHealth;

        public Action<HealthHandler, float, float> OnHealthChanged;

        public virtual void OnDeath() => Destroy(this);
    }
}
