using GameCore.Save;
using Player;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Menu.Shop
{
    public class GameShop : MonoBehaviour
    {
        [SerializeField] private TMP_Text _healthCostText;
        [SerializeField] private TMP_Text _speedCostText;
        [SerializeField] private TMP_Text _regenCostText;
        [SerializeField] private TMP_Text _rangeCostText;

        [SerializeField] private Button _healthButton;
        [SerializeField] private Button _speedButton;
        [SerializeField] private Button _regenButton;
        [SerializeField] private Button _rangeButton;
        
        private UpgradeLoader _upgradeLoader;
        private PlayerData _playerData;
        private MenuUIUpdater _menuUIUpdater;
        private SaveProgress _saveProgress;

        [Inject]
        private void Construct(UpgradeLoader upgradeLoader, PlayerData playerData, MenuUIUpdater menuUIUpdater,
            SaveProgress saveProgress)
        {
            _upgradeLoader = upgradeLoader;
            _playerData = playerData;
            _menuUIUpdater = menuUIUpdater;
            _saveProgress = saveProgress;
        }

        public void TryUpgrade(int id)
        {
            switch (id)
            {
                case 1:
                    TrySpendCoins(_upgradeLoader.HealthCurrentLevel);
                    if (_playerData.MaxHealthUpgradeIndex < 5)
                    {
                        _playerData.SetUpgradeIndex(_playerData.MaxHealthUpgradeIndex + 1, 1);
                    }
                    _upgradeLoader.LoadCurrentLevels();
                    CheckButtons();
                    ShowPrice();
                    break;
                case 2:
                    TrySpendCoins(_upgradeLoader.SpeedCurrentLevel);
                    if (_playerData.SpeedUpgradeIndex < 5)
                    {
                        _playerData.SetUpgradeIndex(_playerData.SpeedUpgradeIndex + 1, 2);
                    }
                    _upgradeLoader.LoadCurrentLevels();
                    CheckButtons();
                    ShowPrice();
                    break;
                case 3:
                    TrySpendCoins(_upgradeLoader.RegenCurrentLevel);
                    if (_playerData.RegenerationUpgradeIndex < 5)
                    {
                        _playerData.SetUpgradeIndex(_playerData.RegenerationUpgradeIndex + 1, 3);
                    }
                    _upgradeLoader.LoadCurrentLevels();
                    CheckButtons();
                    ShowPrice();
                    break;
                case 4:
                    TrySpendCoins(_upgradeLoader.RangeCurrentLevel);
                    if (_playerData.ExpRangeUpgradeIndex < 5)
                    {
                        _playerData.SetUpgradeIndex(_playerData.ExpRangeUpgradeIndex + 1, 4);
                    }
                    _upgradeLoader.LoadCurrentLevels();
                    CheckButtons();
                    ShowPrice();
                    break;
            }
        }

        public void ShowPrice()
        {
            _healthCostText.text = "Cost: " + _upgradeLoader.HealthCurrentLevel.Cost;
            _speedCostText.text = "Cost: " + _upgradeLoader.SpeedCurrentLevel.Cost;
            _regenCostText.text = "Cost: " + _upgradeLoader.RegenCurrentLevel.Cost;
            _rangeCostText.text = "Cost: " + _upgradeLoader.RangeCurrentLevel.Cost;
        }

        public void CheckButtons()
        {
            _healthButton.interactable = _playerData.Coins >= _upgradeLoader.HealthCurrentLevel.Cost && _playerData.MaxHealthUpgradeIndex < 5;
            _speedButton.interactable = _playerData.Coins >= _upgradeLoader.SpeedCurrentLevel.Cost && _playerData.SpeedUpgradeIndex < 5;
            _regenButton.interactable = _playerData.Coins >= _upgradeLoader.RegenCurrentLevel.Cost && _playerData.RegenerationUpgradeIndex < 5;
            _rangeButton.interactable = _playerData.Coins >= _upgradeLoader.RangeCurrentLevel.Cost && _playerData.ExpRangeUpgradeIndex < 5;
        }

        private void TrySpendCoins(ItemShop item)
        {
            _playerData.TrySpendCoins(item.Cost);
            _saveProgress.SaveData();
            _menuUIUpdater.UpdateUI();
        }
    }
}