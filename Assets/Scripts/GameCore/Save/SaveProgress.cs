using Player;
using UnityEngine;
using Zenject;

namespace GameCore.Save
{
    public class SaveProgress
    {
        private PlayerData _playerData;

        [Inject]
        private void Construct(PlayerData playerData)
        {
            _playerData = playerData;
        }

        public void SaveData()
        {
            PlayerPrefs.SetInt("Coins", _playerData.Coins);
            PlayerPrefs.SetInt("Health", _playerData.MaxHealthUpgradeIndex);
            PlayerPrefs.SetInt("Speed", _playerData.SpeedUpgradeIndex);
            PlayerPrefs.SetInt("Regen", _playerData.RegenerationUpgradeIndex);
            PlayerPrefs.SetInt("Range", _playerData.ExpRangeUpgradeIndex);
        }

        public void LoadData()
        {
            _playerData.AddCoins(PlayerPrefs.GetInt("Coins", 0));
            
            _playerData.SetUpgradeIndex(PlayerPrefs.GetInt("Health"), 1);
            if (PlayerPrefs.GetInt("Health") == 0)
            {
                _playerData.SetUpgradeIndex(1, 1);
            }
            _playerData.SetUpgradeIndex(PlayerPrefs.GetInt("Speed"), 2);
            if (PlayerPrefs.GetInt("Speed") == 0)
            {
                _playerData.SetUpgradeIndex(1, 2);
            }
            
            _playerData.SetUpgradeIndex(PlayerPrefs.GetInt("Regen"), 3);
            if (PlayerPrefs.GetInt("Regen") == 0)
            {
                _playerData.SetUpgradeIndex(1, 3);
            }
            
            _playerData.SetUpgradeIndex(PlayerPrefs.GetInt("Range"), 4);
            if (PlayerPrefs.GetInt("Range") == 0)
            {
                _playerData.SetUpgradeIndex(1, 4);
            }
        }
    }
}