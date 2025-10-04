using System;
using System.Collections;
using GameCore.Health;
using GameCore.Pause;
using UnityEngine;
using Zenject;

namespace Player
{
    public class PlayerHealth: ObjectHealth
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private GameObject _endGameWindow;
        private WaitForSeconds _regenerationInterval = new WaitForSeconds(5f);
        private float _regenerationValue = 1f;
        private WaitForSeconds _interval = new WaitForSeconds(1f);
        private GamePause _gamePause;
        
        [Inject]
        private void Construct(GamePause gamePause)
        {
            _gamePause = gamePause;
        }
        
        private void Start()
        {
            StartCoroutine(RegenerateHealth());
        }
        
        public override void TakeDamage(float value)
        {
            base.TakeDamage(value);
            OnHealthChanged?.Invoke();
            
            if (CurrentHealth <= 0)
            {
                StartCoroutine(PlayerDie());
            }
        }

        public void UpgradeHealth()
        {
            currentHealth += 10f;
            maxHealth += 10f;
        }

        public void UpgradeRegeneration()
        {
            _regenerationValue++;
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

        private IEnumerator PlayerDie()
        {
            _gamePause.SetPause(true);
            _animator.SetTrigger("Die");
            yield return _interval;
            _endGameWindow.SetActive(true);
        }
    }
}