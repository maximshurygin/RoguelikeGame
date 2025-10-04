using System.Collections;
using GameCore.Loot;
using GameCore.Pause;
using Player;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace GameCore.UI
{
    public class TreasureWindow : MonoBehaviour, IActivate
    {
        [SerializeField] private GameObject _treasureWindow;
        [SerializeField] private Button _button;
        [SerializeField] private TMP_Text _coinsText;
        
        private GamePause _gamePause;
        private CoinKeeper _coinKeeper;
        private CoinsUIUpdater _coinsUIUpdater;
        private RewardCoinsAnimation _rewardCoinsAnimation;

        private int _randomCoinsToAdd;
        private WaitForSeconds _interval;

        [Inject]
        private void Construct(GamePause gamePause, CoinKeeper coinKeeper, CoinsUIUpdater coinsUIUpdater,
            RewardCoinsAnimation rewardCoinsAnimation)
        {
            _gamePause = gamePause;
            _coinKeeper = coinKeeper;
            _coinsUIUpdater = coinsUIUpdater;
            _rewardCoinsAnimation = rewardCoinsAnimation;
        }

        private void Start()
        {
            _interval = new WaitForSeconds(2.5f);
        }
        
        public void Activate()
        {
            _treasureWindow.SetActive(true);
            _gamePause.SetPause(true);
            _button.gameObject.SetActive(false);
            _randomCoinsToAdd = (int)Random.Range(10f, 100f);
            StartCoroutine(StartCalculate());
        }

        public void Deactivate()
        {
            _treasureWindow.SetActive(false);
            _gamePause.SetPause(false);
        }

        public void GetReward()
        {
            _coinKeeper.AddCoins(_randomCoinsToAdd);
            _coinsUIUpdater.OnCountChange?.Invoke();
        }

        private IEnumerator StartCalculate()
        {
            _rewardCoinsAnimation.ActivateAnimation(_randomCoinsToAdd, 0, _coinsText);
            yield return _interval;
            GetReward();
            _button.gameObject.SetActive(true);
        }
    }
}