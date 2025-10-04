using GameCore.Pause;
using UnityEngine;
using Zenject;

namespace Player.Weapon
{
    public class SwordWeapon : BaseWeapon
    {
        [Header("References")]
        [SerializeField] private Transform _weaponTransform;
        [SerializeField] private BoxCollider2D _swordCollider;
        [SerializeField] private Animator _animator;
        [SerializeField] private Camera _camera;
        
        private float _timeBetweenAttack;
        private float _nextAllowedAttackTime;
        private Vector3 _direction;
        private float _currentAngle;
        private GamePause _gamePause;

        [Inject]
        private void Construct(GamePause gamePause)
        {
            _gamePause = gamePause;
        }
        

        protected override void Start()
        {
            SetStats(0);
            _swordCollider.enabled = false;
        }

        private void Update()
        {
            if (_gamePause.IsStopped) return;

            _direction = _camera.ScreenToWorldPoint(Input.mousePosition) - transform.position;

            float targetAngle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;
            _currentAngle = Mathf.LerpAngle(_currentAngle, targetAngle, Time.deltaTime * 10f);
            _weaponTransform.rotation = Quaternion.AngleAxis(_currentAngle, Vector3.forward);
            
            FlipSpriteBasedOnDirection();
            
            if (Input.GetMouseButtonDown(0))
            {
                Attack();
            }
        }
        
        private void FlipSpriteBasedOnDirection()
        {
            if (_direction.x < 0)
            {
                _weaponTransform.localScale = new Vector3(_weaponTransform.localScale.x, -Mathf.Abs(_weaponTransform.localScale.y), _weaponTransform.localScale.z);
            }
            else
            {
                _weaponTransform.localScale = new Vector3(_weaponTransform.localScale.x, Mathf.Abs(_weaponTransform.localScale.y), _weaponTransform.localScale.z);
            }
        }

        private void Attack()
        {
            if (_gamePause.IsStopped) return;
            if (Time.time < _nextAllowedAttackTime) return;
            _swordCollider.enabled = true;
            _animator.SetTrigger("Attack");
            _nextAllowedAttackTime = Time.time + _timeBetweenAttack;
        }

        private void StopAttacking()
        {
            _swordCollider.enabled = false;
            _animator.SetTrigger("Idle");
        }

        protected override void SetStats(int value)
        {
            base.SetStats(value);
            _timeBetweenAttack = WeaponStats[CurrentLevel - 1].TimeBetweenAttack;
        }
    }
}