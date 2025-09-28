using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace GameCore.ExperienceSystem
{
    public class ExperienceUIUpdater : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _experienceText;
        [SerializeField] private Image _experienceBar;
        private ExperienceSystem _experienceSystem;

        [Inject]
        private void Construct(ExperienceSystem experienceSystem)
        {
            _experienceSystem = experienceSystem;
        }

        private void OnEnable()
        {
            _experienceSystem.OnExperiencePickUp += UpdateExperienceBar;
        }

        private void OnDisable()
        {
            _experienceSystem.OnExperiencePickUp -= UpdateExperienceBar;
        }

        private void Start()
        {
            _experienceBar.fillAmount = 0f;
            _experienceText.text = "1 LVL";
        }

        private void UpdateExperienceBar(float experience)
        {
            _experienceBar.fillAmount = _experienceSystem.CurrentExperience / _experienceSystem.ExperienceToNextLevel;
            _experienceBar.fillAmount = Mathf.Clamp01(_experienceBar.fillAmount);
            _experienceText.text = $"{_experienceSystem.CurrentLevel} LVL";
        }
    }
}