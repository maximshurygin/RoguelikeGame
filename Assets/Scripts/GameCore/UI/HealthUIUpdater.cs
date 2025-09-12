using Player;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace GameCore.UI
{
    public class HealthUIUpdater : MonoBehaviour
    {
        [SerializeField] private Image playerHealthImage;
        private PlayerHealth _playerHealth;
        
        [Inject]
        private void Construct(PlayerHealth playerHealth)
        {
            _playerHealth = playerHealth;
        }

        private void OnEnable()
        {
            _playerHealth.OnHealthChanged += UpdateHealthBar;
        }
        
        private void OnDisable()
        {
            _playerHealth.OnHealthChanged -= UpdateHealthBar;
        }

        private void UpdateHealthBar()
        {
            playerHealthImage.fillAmount = _playerHealth.CurrentHealth / _playerHealth.MaxHealth;
            playerHealthImage.fillAmount = Mathf.Clamp01(playerHealthImage.fillAmount);
        }
    }
}