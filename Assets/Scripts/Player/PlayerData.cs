using System;

namespace Player
{
    public class PlayerData
    {
        public int Coins { get; private set; }
        public int MaxHealthUpgradeIndex { get; private set; }
        public int SpeedUpgradeIndex { get; private set; }
        public int RegenerationUpgradeIndex { get; private set; }
        public int ExpRangeUpgradeIndex { get; private set; }

        public void TrySpendCoins(int amount)
        {
            if (amount <= 0 || amount > Coins)
            {
                throw new ArgumentOutOfRangeException(nameof(amount));
            }
            Coins -= amount;
        }
        
        public void AddCoin()
        {
            Coins++;
        }

        public void AddRewardCoins(int amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount));
            }
            Coins += amount;
        }

        public void SetUpgradeIndex(int value, int id)
        {
            if (value < 0 || value > 5)
            {
                throw new ArgumentOutOfRangeException(nameof(value));
            }

            switch (id)
            {
                case 1:
                    MaxHealthUpgradeIndex = value;
                    break;
                case 2:
                    SpeedUpgradeIndex = value;
                    break;
                case 3:
                    RegenerationUpgradeIndex = value;
                    break;
                case 4:
                    ExpRangeUpgradeIndex = value;
                    break;
            }
        }
    }
}