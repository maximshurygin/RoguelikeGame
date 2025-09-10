using System;
using UnityEngine;

namespace GameCore.Health
{
    public abstract class ObjectHealth : MonoBehaviour, IDamageable
    {
        [SerializeField] private float maxHealth;
        [SerializeField] private float currentHealth;
        
        public float MaxHealth => maxHealth;
        public float CurrentHealth => currentHealth;
        
        protected virtual void OnEnable() => currentHealth = maxHealth;

        public virtual void TakeDamage(float damage)
        {
            if (damage <= 0)
                if (damage <= 0) throw new ArgumentOutOfRangeException(nameof(damage));
            currentHealth -= damage;
        }

        public virtual void TakeHeal(float value)
        {
            if (value <= 0)
                throw new ArgumentOutOfRangeException(nameof(value));
            currentHealth = Mathf.Min(currentHealth + value, maxHealth);
        }
    }
}