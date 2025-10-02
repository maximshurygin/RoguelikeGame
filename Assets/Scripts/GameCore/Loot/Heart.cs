using Player;
using UnityEngine;
using Zenject;

namespace GameCore.Loot
{
    public class Heart : Loot
    {
        private PlayerHealth _playerHealth;
        private float _healAmount;
        private float _healPercentage = 0.25f;

        [Inject]
        private void Construct(PlayerHealth playerHealth)
        {
            _playerHealth = playerHealth;
        }

        private void Start()
        {
            _healAmount = _playerHealth.MaxHealth * _healPercentage;
        }

        protected override void PickUp()
        {
            base.PickUp();
            _playerHealth.TakeHeal(_healAmount);
        }
    }
}