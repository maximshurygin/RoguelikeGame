using GameCore.UI;
using UnityEngine;
using Zenject;

namespace GameCore.Loot
{
    public class Treasure : Loot
    {
        private TreasureWindow _treasureWindow;

        [Inject]
        private void Construct(TreasureWindow treasureWindow)
        {
            _treasureWindow = treasureWindow;
        }

        protected override void PickUp()
        {
            base.PickUp();
            _treasureWindow.Activate();
        }
    }
}