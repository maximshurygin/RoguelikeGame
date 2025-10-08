using System;
using System.Collections;
using TMPro;
using UnityEngine;
using Zenject;

namespace GameCore.LevelSystem
{
    public class GameTimer : MonoBehaviour, IActivate
    {
        private LevelSystem _levelSystem;
        private WaitForSeconds _tick = new WaitForSeconds(1f);
        private Coroutine _timerCoroutine;
        private int _seconds, _minutes;
        
        public event Action OnTimerFinished;
        public event Action<int, int> OnTimeChanged;

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
            while (_minutes < 15)
            {
                _seconds++;
                if (_seconds >= 60)
                {
                    _minutes++;
                    _seconds = 0;
                    _levelSystem.OnLevelChanged?.Invoke();
                }
                OnTimeChanged?.Invoke(_minutes, _seconds);
                yield return _tick;
            }
            OnTimerFinished?.Invoke();
        }
        
    }
}