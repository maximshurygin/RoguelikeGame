using GameCore.LevelSystem;
using TMPro;
using UnityEngine;
using Zenject;

namespace GameCore.UI
{
    public class GameTimerUIUpdater : MonoBehaviour
    {
        [SerializeField] private TMP_Text _gameTimerText;
        private GameTimer _gameTimer;

        [Inject]
        private void Construct(GameTimer gameTimer)
        {
            _gameTimer = gameTimer;
        }

        private void OnEnable()
        {
            _gameTimer.OnTimeChanged += TimeTextFormat;
        }

        private void OnDisable()
        {
            _gameTimer.OnTimeChanged -= TimeTextFormat;
        }
        
        private void TimeTextFormat(int minutes, int seconds)
        {
            string minutesString = minutes < 10 ? $"0{minutes}" : minutes.ToString();
            string secondsString = seconds < 10 ? $"0{seconds}" : seconds.ToString();
            _gameTimerText.text = $"{minutesString}:{secondsString}";
        }
    }
}