using System;
using GameCore.UpgradeSystem;
using UnityEngine;

namespace GameCore.ExperienceSystem
{
    public class ExperienceSystem : MonoBehaviour
    {
        public Action<float> OnExperiencePickUp;
        [SerializeField] private GameObject _upgradeWindow;
        [SerializeField] private float _currentExperience;
        [SerializeField] private AudioSource _audioSource;
        private float _experienceToNextLevel = 5f;
        private int _currentLevel = 1;

        public float CurrentExperience => _currentExperience;
        public float ExperienceToNextLevel => _experienceToNextLevel;
        public int CurrentLevel => _currentLevel;

        public void ExperienceAddValue(float value)
        {
            if (value <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(value));
            }
            _currentExperience += value;
            if (_currentExperience >= _experienceToNextLevel)
            {
                LevelUp();
            }
        }

        private void LevelUp()
        {
            _currentExperience = 0;
            _currentLevel++;
            _experienceToNextLevel += 2;
            _audioSource.PlayOneShot(_audioSource.clip);
            _upgradeWindow.SetActive(true);
            _upgradeWindow.GetComponent<UpgradeWindow>().GetRandomCards();
        }
    }
}