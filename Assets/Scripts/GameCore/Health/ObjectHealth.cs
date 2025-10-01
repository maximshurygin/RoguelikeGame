using System;
using UnityEngine;

namespace GameCore.Health
{
    public abstract class ObjectHealth : MonoBehaviour, IDamageable
    {
        [SerializeField] protected float maxHealth;
        [SerializeField] protected float currentHealth;
        [SerializeField] private Animator damageAnimator;
        [SerializeField] private AudioSource damageAudioSource;
        [SerializeField] private DamageFlash damageFlash;
        public Action OnHealthChanged;
        
        public float MaxHealth => maxHealth;
        public float CurrentHealth => currentHealth;

        protected virtual void OnEnable()
        {
            currentHealth = maxHealth;
        }

        public virtual void TakeDamage(float damage)
        {
            if (damage <= 0) throw new ArgumentOutOfRangeException(nameof(damage));
            currentHealth -= damage;
            
            damageFlash?.Flash();
            damageAnimator?.Play("Blood", 0, 0f);

            if (damageAudioSource && damageAudioSource.clip)
            {
                if (currentHealth <= 0f)
                    AudioSource.PlayClipAtPoint(damageAudioSource.clip, transform.position);
                else
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