using System.Collections;
using GameCore.Loot;
using GameCore.Pause;
using GameCore.Save;
using GameCore.ScenesLoader;
using Player;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace GameCore.UI
{
    public class EndGameWindow : MonoBehaviour
    {
        [SerializeField] private Button _endButton;
        [SerializeField] private TMP_Text _coinsText;
        [SerializeField] private TMP_Text _gameOverText;
        [SerializeField] private TMP_Text _winText;
        private WaitForSeconds _interval;
        private int _coins;
        
        private CoinKeeper _coinKeeper;
        private RewardCoinsAnimation _rewardCoinsAnimation;
        private PlayerData _playerData;
        private SaveProgress _saveProgress;
        private GamePause _gamePause;
        private SceneLoader _sceneLoader;
        private EndGame.EndGame _endGame;
        
        [Inject]
        private void Construct(RewardCoinsAnimation rewardCoinsAnimation, CoinKeeper coinKeeper,
            PlayerData playerData, SaveProgress saveProgress, GamePause gamePause, SceneLoader sceneLoader, EndGame.EndGame endGame)
        {
            _rewardCoinsAnimation = rewardCoinsAnimation;
            _coinKeeper = coinKeeper;
            _playerData = playerData;
            _saveProgress = saveProgress;
            _gamePause = gamePause;
            _sceneLoader = sceneLoader;
            _endGame = endGame;
        }
        
        private void OnEnable()
        {
            if (_endGame.IsWinner)
            {
                _winText.gameObject.SetActive(true);
            }
            else
            {
                _gameOverText.gameObject.SetActive(true);
            }
            _gamePause.SetPause(true);
            _endButton.gameObject.SetActive(false);
            _coins = _coinKeeper.Coins;
            _coinsText.text = "0";
            _interval = new WaitForSeconds(2.5f);
            StartCoroutine(CalculateCoins());
        }

        public void ExitGame()
        {
            _playerData.AddCoins(_coins);
            _saveProgress.SaveData();
            _sceneLoader.MainMenu();
        }

        private IEnumerator CalculateCoins()
        {
            if (_coins > 10)
            {
                _rewardCoinsAnimation.ActivateAnimation(_coins, 0, _coinsText);
                yield return _interval;
            }
            else
            {
                _coinsText.text = _coins.ToString();
            }
            _endButton.gameObject.SetActive(true);
        }
    }
}