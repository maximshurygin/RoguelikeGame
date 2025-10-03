using GameCore.UI;
using Player;
using UnityEngine;
using Zenject;

namespace GameCore.Loot
{
    public class Coin: Loot
    {
        private CoinsUIUpdater _coinsUIUpdater;
        private CoinKeeper _coinsKeeper;
        private PlayerData _playerData;

        [Inject]
        private void Construct(CoinsUIUpdater coinsUIUpdater, CoinKeeper coinsKeeper, PlayerData playerData)
        {
            _coinsUIUpdater = coinsUIUpdater;
            _coinsKeeper = coinsKeeper;
            _playerData = playerData;
        }

        protected override void PickUp()
        {
            base.PickUp();
            _coinsKeeper.AddCoin();
            _coinsUIUpdater.OnCountChange?.Invoke();
            _playerData.AddCoin();
        }
    }
}