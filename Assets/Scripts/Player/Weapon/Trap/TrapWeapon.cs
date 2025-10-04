using System.Collections;
using GameCore;
using GameCore.GameObjectPool;
using GameCore.Pause;
using UnityEngine;
using Zenject;

namespace Player.Weapon.Trap
{
    public class TrapWeapon : BaseWeapon, IActivate
    {
        [SerializeField] private ObjectPool _objectPool;
        [SerializeField] private Transform _container;
        private WaitForSeconds _timeBetweenAttack;
        private Coroutine _trapCoroutine;
        private float _slowdownRate;
        private WaitForSeconds _slowdownDuration;
        private GamePause _gamePause;

        public WaitForSeconds SlowdownDuration => _slowdownDuration;
        public float SlowdownRate => _slowdownRate;

        [Inject]
        private void Construct(GamePause gamePause)
        {
            _gamePause = gamePause;
        }

        private void OnEnable()
        {
            Activate();
        }
        
        public void Activate()
        {
            SetStats(0);
            _trapCoroutine = StartCoroutine(SpawnTrap());
        }

        public void Deactivate()
        {
            if (_trapCoroutine != null)
            {
                StopCoroutine(_trapCoroutine);
            }
        }

        protected override void SetStats(int value)
        {
            base.SetStats(value);
            _timeBetweenAttack = new WaitForSeconds(WeaponStats[CurrentLevel - 1].TimeBetweenAttack);
            _slowdownDuration = new WaitForSeconds(WeaponStats[CurrentLevel - 1].SlowdownDuration);
            _slowdownRate = WeaponStats[CurrentLevel - 1].SlowdownRate;
        }

        private IEnumerator SpawnTrap()
        {
            while (true)
            {
                while (_gamePause.IsStopped)
                {
                    yield return null;
                }
                GameObject trap = _objectPool.GetFromPool();
                trap.transform.SetParent(_container);
                trap.transform.position = transform.position;
                yield return _timeBetweenAttack;
            }
        }
    }
}