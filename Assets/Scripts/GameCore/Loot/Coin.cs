using GameCore.UI;
using UnityEngine;
using Zenject;

namespace GameCore.Loot
{
    public class Coin: Loot
    {
        private CoinsUIUpdater _coinsUIUpdater;
        private CoinKeeper _coinsKeeper;

        [Inject]
        private void Construct(CoinsUIUpdater coinsUIUpdater, CoinKeeper coinsKeeper)
        {
            _coinsUIUpdater = coinsUIUpdater;
            _coinsKeeper = coinsKeeper;
        }

        protected override void PickUp()
        {
            base.PickUp();
            _coinsKeeper.AddCoin();
            _coinsUIUpdater.OnCountChange?.Invoke();
        }
    }
}