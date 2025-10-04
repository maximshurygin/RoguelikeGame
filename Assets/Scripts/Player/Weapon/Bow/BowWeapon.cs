using GameCore.GameObjectPool;
using GameCore.Pause;
using UnityEngine;
using Zenject;

namespace Player.Weapon.Bow
{
    public class BowWeapon: BaseWeapon
    {
        [Header("References")]
        [SerializeField] private Camera _camera;
        [SerializeField] private Transform _container;
        [SerializeField] private Transform _shootPoint;
        [SerializeField] private Transform _weaponTransform;
        [SerializeField] private ObjectPool _objectPool;
        [SerializeField] private Animator _animator;

        [Header("Charge Settings")]
        [SerializeField] private float _minMultiplier = 0.6f;
        [SerializeField] private float _maxMultiplier = 2.0f;
        [SerializeField] private float _chargeTime = 3.0f;

        private Vector3 _direction;
        private float _duration;
        private float _speed;
        private float _timeBetweenAttack;
        private float _nextAllowedShotTime;

        private bool _isCharging;
        private float _chargeMultiplier;
        
        private GamePause _gamePause;

        public float Duration => _duration;
        public float Speed => _speed;
        public float ChargedDamage => Damage * _chargeMultiplier;


        [Inject]
        private void Construct(GamePause gamePause)
        {
            _gamePause = gamePause;
        }
        
        protected override void Start()
        {
            SetStats(0);
            _chargeMultiplier = _minMultiplier;
        }

        private void Update()
        {
            if (_gamePause.IsStopped) return;
            Aim();
            HandleCharge();
        }

        private void Aim()
        {
            _direction = _camera.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            float angle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;
            _weaponTransform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }

        private void HandleCharge()
        {
            // Начало заряда
            if (Input.GetMouseButtonDown(0) && Time.time >= _nextAllowedShotTime)
            {
                _isCharging = true;
                _chargeMultiplier = _minMultiplier;
                _animator?.SetTrigger("Charging");
            }

            // Увеличение заряда
            if (_isCharging && Input.GetMouseButton(0))
            {
                float chargeIncrease = (_maxMultiplier - _minMultiplier) / _chargeTime * Time.deltaTime;
                _chargeMultiplier = Mathf.Min(_chargeMultiplier + chargeIncrease, _maxMultiplier);
            }

            // Выстрел
            if (_isCharging && Input.GetMouseButtonUp(0))
            {
                _isCharging = false;
                ThrowArrow();
                _nextAllowedShotTime = Time.time + _timeBetweenAttack;
                _animator?.SetTrigger("Attack");
            }
        }

        private void ThrowArrow()
        {
            GameObject arrow = _objectPool.GetFromPool();
            arrow.transform.SetParent(_container);
            arrow.transform.position = _shootPoint.position;
            arrow.transform.rotation = _shootPoint.rotation;
            _animator?.SetTrigger("Idle");
        }

        protected override void SetStats(int value)
        {
            base.SetStats(value);
            _timeBetweenAttack = WeaponStats[CurrentLevel - 1].TimeBetweenAttack;
            _speed = WeaponStats[CurrentLevel - 1].Speed;
            _duration = WeaponStats[CurrentLevel - 1].Duration;
        }
    }
}