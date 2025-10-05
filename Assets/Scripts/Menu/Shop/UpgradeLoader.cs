using System;
using System.Collections.Generic;
using GameCore.Save;
using Player;
using UnityEngine;
using Zenject;

namespace Menu.Shop
{
    public class UpgradeLoader : MonoBehaviour
    {
        [SerializeField] private List<ItemShop> _maxHealthLevels = new List<ItemShop>();
        [SerializeField] private List<ItemShop> _speedLevels = new List<ItemShop>();
        [SerializeField] private List<ItemShop> _regenLevels = new List<ItemShop>();
        [SerializeField] private List<ItemShop> _rangeLevels = new List<ItemShop>();
        [SerializeField] private List<ItemShop> _dropLevels = new List<ItemShop>();


        public ItemShop HealthCurrentLevel { get; private set; }
        public ItemShop SpeedCurrentLevel { get; private set; }
        public ItemShop RegenCurrentLevel { get; private set; }
        public ItemShop RangeCurrentLevel { get; private set; }
        public ItemShop DropCurrentLevel { get; private set; }
        
        private PlayerData _playerData;
        private SaveProgress _saveProgress;

        [Inject]
        private void Construct(PlayerData playerData, SaveProgress saveProgress)
        {
            _playerData = playerData;
            _saveProgress = saveProgress;
        }

        private void Awake()
        {
            _saveProgress.LoadData();
            LoadCurrentLevels();
        }

        public void LoadCurrentLevels()
        {
            HealthCurrentLevel = _maxHealthLevels[_playerData.MaxHealthUpgradeIndex - 1];
            SpeedCurrentLevel = _speedLevels[_playerData.SpeedUpgradeIndex - 1];
            RegenCurrentLevel = _regenLevels[_playerData.RegenerationUpgradeIndex - 1];
            RangeCurrentLevel = _rangeLevels[_playerData.ExpRangeUpgradeIndex - 1];
            DropCurrentLevel = _dropLevels[_playerData.DropChanceUpgradeIndex - 1];
        }
    }
}