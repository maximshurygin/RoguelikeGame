using System.Collections;
using GameCore;
using GameCore.GameObjectPool;
using GameCore.Pause;
using UnityEngine;
using Zenject;

namespace Player.Weapon.Suriken
{
    public class SurikenWeapon : BaseWeapon, IActivate
    {
        [SerializeField] private ObjectPool _objectPool;
        [SerializeField] private Transform _container;
        [SerializeField] private LayerMask _layerMask;
        private WaitForSeconds _timeBetweenAttack;
        private Coroutine _surikenCoroutine;
        private float _duration, _speed, _range;
        private Vector3 _direction;
        private GamePause _gamePause;
        public float Duration => _duration;
        public float Speed => _speed;

        [Inject]
        private void Construct(GamePause gamePause)
        {
            _gamePause = gamePause;
        }

        private void OnEnable() => Activate();

        public void Activate()
        {
            SetStats(0);
            _surikenCoroutine = StartCoroutine(SpawnSuriken());
        }

        public void Deactivate()
        {
            if (_surikenCoroutine != null)
            {
                StopCoroutine(_surikenCoroutine);
            }
        }

        protected override void SetStats(int value)
        {
            base.SetStats(CurrentLevel -1);
            _timeBetweenAttack = new WaitForSeconds(WeaponStats[CurrentLevel - 1].TimeBetweenAttack);
            _speed = WeaponStats[CurrentLevel - 1].Speed;
            _range = WeaponStats[CurrentLevel - 1].Range;
            _duration = WeaponStats[CurrentLevel - 1].Duration;
            _lvlText.text = CurrentLevel.ToString();
        }

        private IEnumerator SpawnSuriken()
        {
            while (true)
            {
                while (_gamePause.IsStopped)
                {
                    yield return null;
                }
                
                Collider2D[] enemiesInRange = Physics2D.OverlapCircleAll(transform.position, _range, _layerMask);
                if (enemiesInRange.Length > 0)
                {
                    Vector3 targetPosition = enemiesInRange[Random.Range(0, enemiesInRange.Length)].transform.position;
                    _direction = (targetPosition - transform.position).normalized;
                    float angle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;
                    GameObject suriken = _objectPool.GetFromPool();
                    suriken.transform.SetParent(_container);
                    suriken.transform.position = transform.position;
                    suriken.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                    yield return _timeBetweenAttack;
                }
                else
                    yield return _timeBetweenAttack;
            }
        }
    }
}