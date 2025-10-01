using System;
using GameCore.Loot;
using TMPro;
using UnityEngine;
using Zenject;

namespace GameCore.UI
{
    public class CoinsUIUpdater : MonoBehaviour
    {
        public Action OnCountChange;
        
        [SerializeField] private TMP_Text _coinsText;
        private CoinKeeper _coinKeeper;

        [Inject]
        private void Construct(CoinKeeper coinKeeper)
        {
            _coinKeeper = coinKeeper;
        }

        private void OnEnable()
        {
            OnCountChange += UpdateCoinsText;
        }
        
        private void OnDisable()
        {
            OnCountChange -= UpdateCoinsText;
        }

        private void UpdateCoinsText()
        {
            _coinsText.text = _coinKeeper.Coins.ToString();
        }
    }
}