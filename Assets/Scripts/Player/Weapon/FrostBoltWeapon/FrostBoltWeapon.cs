using System.Collections;
using System.Collections.Generic;
using GameCore;
using GameCore.GameObjectPool;
using UnityEngine;

namespace Player.Weapon.FrostBoltWeapon
{
    public class FrostBoltWeapon : BaseWeapon, IActivate
    {
        [SerializeField] private ObjectPool _objectPool;
        [SerializeField] private Transform _container;
        [SerializeField] private List<Transform> _shootPoints =  new List<Transform>();
        private WaitForSeconds _timeBetweenAttack;
        private Coroutine _frostBoltCoroutine;
        private float _duration, _speed, _slowdownRate;
        private WaitForSeconds _slowdownDuration;
        private Vector3 _direction;

        public float Duration => _duration;
        public float Speed => _speed;
        public float SlowdownRate => _slowdownRate;
        public WaitForSeconds SlowdownDuration => _slowdownDuration;


        private void OnEnable()
        {
            Activate();
        }
        
        public void Activate()
        {
            SetStats(0);
            _frostBoltCoroutine = StartCoroutine(ThrowFrostBolt());
        }

        public void Deactivate()
        {
            if (_frostBoltCoroutine != null)
            {
                StopCoroutine(_frostBoltCoroutine);
            }
        }

        protected override void SetStats(int value)
        {
            base.SetStats(value);
            _timeBetweenAttack = new WaitForSeconds(WeaponStats[CurrentLevel - 1].TimeBetweenAttack);
            _speed = WeaponStats[CurrentLevel - 1].Speed;
            _duration = WeaponStats[CurrentLevel - 1].Duration;
            _slowdownDuration = new WaitForSeconds(WeaponStats[CurrentLevel - 1].SlowdownDuration);
            _slowdownRate = WeaponStats[CurrentLevel - 1].SlowdownRate;
        }
        
        private IEnumerator ThrowFrostBolt()
        {
            while (true)
            {
                for (int i = 0; i < _shootPoints.Count; i++)
                {
                    _direction = (_shootPoints[i].position - transform.position).normalized;
                    float angle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;
                    GameObject frostBolt = _objectPool.GetFromPool();
                    frostBolt.transform.SetParent(_container);
                    frostBolt.transform.position = transform.position;
                    frostBolt.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                }

                yield return _timeBetweenAttack;
            }
        }
    }
}