using GameCore.Save;
using Player;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Menu.Shop
{
    public class Test : MonoBehaviour
    {
        [SerializeField] private Button _button;
        private PlayerData _playerData;
        private SaveProgress _saveProgress;
        
        [Inject]
        private void Construct(PlayerData playerData, SaveProgress saveProgress)
        {
            _playerData = playerData;
            _saveProgress = saveProgress;
        }

        public void AddCoins()
        {
            _playerData.AddCoins(1000);
        }

        public void ResetAllData()
        {
            _playerData.ResetAllData();
            _saveProgress.SaveData();
            _saveProgress.LoadData();
        }
    }
}