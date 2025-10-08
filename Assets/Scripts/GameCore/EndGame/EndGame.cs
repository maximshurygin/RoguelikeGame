using GameCore.LevelSystem;
using UnityEngine;
using Zenject;

namespace GameCore.EndGame
{
    public class EndGame : MonoBehaviour
    {
        [SerializeField] private GameObject _endGameWindow;
        private GameTimer _gameTimer;
        private bool _isWinner = false;

        public bool IsWinner => _isWinner;

        [Inject]
        private void Construct(GameTimer gameTimer)
        {
            _gameTimer = gameTimer;
        }

        private void OnEnable()
        {
            _gameTimer.OnTimerFinished += WinGame;
        }

        private void OnDisable()
        {
            _gameTimer.OnTimerFinished -= WinGame;
        }

        private void WinGame()
        {
            _isWinner = true;
            _endGameWindow.SetActive(true);
        }
    }
}