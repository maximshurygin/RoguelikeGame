using GameCore.LevelSystem;
using UnityEngine;
using Zenject;

namespace GameCore.Pause
{
    public class GamePause : MonoBehaviour
    {
        private LevelSystem.LevelSystem _levelSystem;
        private GameTimer _gameTimer;
        public bool IsStopped { get; private set; }

        [Inject]
        private void Construct(LevelSystem.LevelSystem levelSystem, GameTimer gameTimer)
        {
            _levelSystem = levelSystem;
            _gameTimer = gameTimer;
        }
        
        public void SetPause(bool value)
        {
            if (value)
            {
                PauseOn();
            }
            else
            {
                PauseOff();
            }
        }

        private void PauseOn()
        {
            _levelSystem.Deactivate();
            _gameTimer.Deactivate();
            IsStopped = true;
        }

        private void PauseOff()
        {
            _levelSystem.Activate();
            _gameTimer.Activate();
            IsStopped = false;
        }
    }
}