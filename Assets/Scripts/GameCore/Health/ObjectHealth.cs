using System;
using UnityEngine;

namespace GameCore.Health
{
    public abstract class ObjectHealth : MonoBehaviour, IDamageable
    {
        [SerializeField] private float maxHealth;
        [SerializeField] private float currentHealth;
        [SerializeField] private Animator damageAnimator;
        [SerializeField] private AudioSource damageAudioSource;
        public Action OnHealthChanged;
        
        public float MaxHealth => maxHealth;
        public float CurrentHealth => currentHealth;

        protected virtual void OnEnable()
        {
            currentHealth = maxHealth;
            damageAnimator.enabled = true;
        }

        protected virtual void OnDisable()
        {
            damageAnimator.enabled = false;
        }

        public virtual void TakeDamage(float damage)
        {
            if (damage <= 0)
                if (damage <= 0) throw new ArgumentOutOfRangeException(nameof(damage));
            currentHealth -= damage;
            
            damageAnimator?.SetTrigger("Hit");
            if (damageAudioSource && damageAudioSource.clip)
            {
                damageAudioSource.PlayOneShot(damageAudioSource.clip);
            }
        }

        public virtual void TakeHeal(float value)
        {
            if (value <= 0)
                throw new ArgumentOutOfRangeException(nameof(value));
            currentHealth = Mathf.Min(currentHealth + value, maxHealth);
        }
    }
}