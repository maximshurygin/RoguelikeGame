using GameCore.UpgradeSystem;
using Player;
using UnityEngine;
using Zenject;

namespace GameCore.ExperienceSystem
{
    public class Experience : MonoBehaviour
    {
        private float _expValue;

        public float ExpValue
        {
            get => _expValue;
            set => _expValue = value;
        }
        private ExperienceSystem _experienceSystem;
        private PlayerHealth _playerHealth;
        private PlayerUpgrade _playerUpgrade;
        private float _distanceToPickUp;

        [Inject]
        private void Construct(ExperienceSystem experienceSystem, PlayerHealth playerHealth,
            PlayerUpgrade playerUpgrade)
        {
            _experienceSystem = experienceSystem;
            _playerHealth = playerHealth;
            _playerUpgrade = playerUpgrade;
        }

        private void OnEnable()
        {
            _distanceToPickUp = _playerUpgrade.RangeExp;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out PlayerHealth playerHealth))
            {
                _experienceSystem.ExperienceAddValue(_expValue);
                _experienceSystem.OnExperiencePickUp?.Invoke(_expValue);
                gameObject.SetActive(false);
            }
        }

        private void Update()
        {
            if (Vector3.Distance(transform.position, _playerHealth.transform.position) <= _distanceToPickUp)
            {
                transform.position = Vector3.MoveTowards(transform.position, _playerHealth.transform.position, 2f * Time.deltaTime);
            }
        }
    }
}