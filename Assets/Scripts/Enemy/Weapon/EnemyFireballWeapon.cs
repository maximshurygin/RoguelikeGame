using System.Collections;
using GameCore;
using GameCore.GameObjectPool;
using Player;
using Player.Weapon;
using UnityEngine;

namespace Enemy.Weapon
{
    public class EnemyFireballWeapon : BaseWeapon, IActivate
    {
        [SerializeField] private ObjectPool _objectPool;
        [SerializeField] private Transform _container;
        [SerializeField] private LayerMask _layerMask;
        private WaitForSeconds _timeBetweenAttack;
        private Coroutine _fireballCoroutine;
        private float _duration, _speed, _range;
        private Vector3 _direction;

        private void OnEnable() => Activate();

        public void Activate()
        {
            SetStats(0);
            _fireballCoroutine = StartCoroutine(SpawnFireball());
        }
        
        public void Deactivate()
        {
            if (_fireballCoroutine != null)
            {
                StopCoroutine(_fireballCoroutine);
            }
        }
        
        protected override void SetStats(int value)
        {
            base.SetStats(CurrentLevel - 1);
            _timeBetweenAttack = new WaitForSeconds(WeaponStats[CurrentLevel - 1].TimeBetweenAttack);
            _speed = WeaponStats[CurrentLevel - 1].Speed;
            _range = WeaponStats[CurrentLevel - 1].Range;
            _duration = WeaponStats[CurrentLevel - 1].Duration;
        }

        private IEnumerator SpawnFireball()
        {
            while (true)
            {
                var playerObj = Physics2D.OverlapCircle(transform.position, _range, _layerMask);
                if (playerObj != null && playerObj.TryGetComponent(out PlayerHealth player))
                {
                    Vector3 targetPosition = playerObj.transform.position;
                    _direction = (targetPosition - transform.position).normalized;
                    float angle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;
                    GameObject fireballObject = _objectPool.GetFromPool();
                    fireballObject.transform.SetParent(_container);
                    fireballObject.transform.position = transform.position;
                    fireballObject.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                    fireballObject.transform.right = _direction;

                    if (fireballObject.TryGetComponent(out EnemyFireball fireball))
                    {
                        fireball.Init(_duration, _speed, Damage);
                    }
                }
                yield return _timeBetweenAttack;
            }
        }
    }
}