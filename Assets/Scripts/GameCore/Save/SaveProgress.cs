using System.IO;
using System.Text;
using Player;
using UnityEngine;
using Zenject;

namespace GameCore.Save
{
    public class SaveProgress
    {
        private PlayerData _playerData;
        private readonly string _savePath = Path.Combine(Application.persistentDataPath, "save.json");


        [Inject]
        private void Construct(PlayerData playerData)
        {
            _playerData = playerData;
        }

        public void SaveData()
        {
            var data = new SaveDataModel
            {
                Coins = _playerData.Coins,
                Health = _playerData.MaxHealthUpgradeIndex,
                Speed = _playerData.SpeedUpgradeIndex,
                Regen = _playerData.RegenerationUpgradeIndex,
                Range = _playerData.ExpRangeUpgradeIndex
            };
            
            string json = JsonUtility.ToJson(data, true);
            File.WriteAllText(_savePath, json, Encoding.UTF8);
        }

        public void LoadData()
        {
            if (!File.Exists(_savePath))
            {
                ApplyDefaults();
                return;
            }
            
            string json = File.ReadAllText(_savePath, Encoding.UTF8);
            var data = JsonUtility.FromJson<SaveDataModel>(json);
            
            _playerData.AddCoins(data.Coins);
            SetOrDefault(data.Health, 1, 1);
            SetOrDefault(data.Speed, 1, 2);
            SetOrDefault(data.Regen, 1, 3);
            SetOrDefault(data.Range, 1, 4);
        }

        private void SetOrDefault(int value, int defaultValue, int index)
        {
            if (value <= 0)
            {
                _playerData.SetUpgradeIndex(defaultValue, index);
            }
            else
            {
                _playerData.SetUpgradeIndex(value, index);
            }
        }

        private void ApplyDefaults()
        {
            _playerData.AddCoins(0);
            _playerData.SetUpgradeIndex(1, 1);
            _playerData.SetUpgradeIndex(1, 2);
            _playerData.SetUpgradeIndex(1, 3);
            _playerData.SetUpgradeIndex(1, 4);
        }
    }
}