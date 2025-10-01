using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace GameCore.UpgradeSystem
{
    public class CardHolder : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _nameText;
        [SerializeField] private TextMeshProUGUI _descriptionText;
        [SerializeField] private UpgradeCard _upgradeCard;
        [SerializeField] private Image _icon;
        
        private UpgradeWindow _upgradeWindow;
        private int _id;

        [Inject]
        private void Construct(UpgradeWindow upgradeWindow)
        {
            _upgradeWindow = upgradeWindow;
        }

        private void Start()
        {
            _nameText.text = _upgradeCard.name;
            _icon.sprite = _upgradeCard.Icon;
            _descriptionText.text = _upgradeCard.Description;
            _id = _upgradeCard.ID;
        }
        
        public void Upgrade()
        {
            _upgradeWindow.Upgrade(_id);
        }
    }
}