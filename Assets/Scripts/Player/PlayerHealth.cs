using System;
using System.Collections;
using GameCore.Health;
using UnityEngine;

namespace Player
{
    public class PlayerHealth: ObjectHealth
    {
        public Action OnHealthChanged;
        private WaitForSeconds _regenerationInterval = new WaitForSeconds(5f);
        private float _regenerationValue = 1f;
        
        private void Start()
        {
            StartCoroutine(RegenerateHealth());
        }

        public void Heal(float amount)
        {
            TakeHeal(amount);
            OnHealthChanged?.Invoke();
        }
        
        public override void TakeDamage(float value)
        {
            base.TakeDamage(value);
            OnHealthChanged?.Invoke();
            
            if (CurrentHealth <= 0)
            {
                Debug.Log("Игрок умер");
            }
        }

        private IEnumerator RegenerateHealth()
        {
            while (enabled)
            {
                yield return _regenerationInterval;
                TakeHeal(_regenerationValue);
                OnHealthChanged?.Invoke();
            }
        }
        
    }
}