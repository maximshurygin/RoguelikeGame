using Player;
using TMPro;
using UnityEngine;
using Zenject;

namespace Menu
{
    public class MenuUIUpdater : MonoBehaviour
    {
        [SerializeField] private TMP_Text _coinsText;
        private PlayerData _playerData;

        [Inject]
        private void Construct(PlayerData playerData)
        {
            _playerData = playerData;
        }

        public void UpdateUI()
        {
            _coinsText.text = _playerData.Coins.ToString();
        }
    }
}