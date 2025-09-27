using System.Collections;
using TMPro;
using UnityEngine;
using Zenject;

namespace GameCore.LevelSystem
{
    public class GameTimer : MonoBehaviour, IActivate
    {
        [SerializeField] private TMP_Text _gameTimerText;
        private LevelSystem _levelSystem;
        private WaitForSeconds _tick = new WaitForSeconds(1f);
        private Coroutine _timerCoroutine;
        private int _seconds, _minutes;

        public int Minutes => _minutes;

        [Inject]
        private void Construct(LevelSystem levelSystem)
        {
            _levelSystem = levelSystem;
        }

        private void Start()
        {
            Activate();
        }

        public void Activate()
        {
            _timerCoroutine = StartCoroutine(Timer());
        }

        public void Deactivate()
        {
            if (_timerCoroutine != null)
            {
                StopCoroutine(_timerCoroutine);
            }
        }

        private IEnumerator Timer()
        {
            while (true)
            {
                _seconds++;
                if (_seconds >= 60)
                {
                    _minutes++;
                    _seconds = 0;
                    _levelSystem.OnLevelChanged?.Invoke();
                }
                TimeTextFormat();
                yield return _tick;
            }
        }

        private void TimeTextFormat()
        {
            string minutesString = _minutes < 10 ? $"0{_minutes}" : _minutes.ToString();
            string secondsString = _seconds < 10 ? $"0{_seconds}" : _seconds.ToString();
            _gameTimerText.text = $"{minutesString}:{secondsString}";
        }
    }
}