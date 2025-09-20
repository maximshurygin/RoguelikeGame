using System.Collections;
using System.Collections.Generic;
using Enemy;
using GameCore;
using UnityEngine;

namespace Player.Weapon
{
    public class AuraWeapon : BaseWeapon, IActivate
    {
        [SerializeField] private Transform _targetContainer;
        [SerializeField] private CircleCollider2D _collider;
        private List<EnemyHealth> _enemyInZone = new List<EnemyHealth>();
        private WaitForSeconds _timeBetweenAttack;
        private Coroutine _auraCoroutine;
        private float _range;

        protected override void Start()
        {
            base.Start();
            Activate();
        }
        
        protected override void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent(out EnemyHealth enemy))
            {
                _enemyInZone.Add(enemy);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent(out EnemyHealth enemy))
            {
                _enemyInZone.Remove(enemy);
            }
        }
        
        public void Activate()
        {
            SetStats(0);
            _auraCoroutine = StartCoroutine(CheckZone());
        }

        public void Deactivate()
        {
            if (_auraCoroutine != null)
            {
                StopCoroutine(_auraCoroutine);
            }
        }

        protected override void SetStats(int value)
        {
            base.SetStats(value);
            _timeBetweenAttack = new WaitForSeconds(WeaponStats[CurrentLevel - 1].TimeBetweenAttack);
            _range = WeaponStats[CurrentLevel - 1].Range;
            _targetContainer.transform.localScale = Vector3.one * _range;
            _collider.radius = _range / 3f;
        }

        private IEnumerator CheckZone()
        {
            while (true)
            {
                for (int i = 0; i < _enemyInZone.Count; i++)
                {
                    _enemyInZone[i].TakeDamage(Damage);
                }

                yield return _timeBetweenAttack;
            }
        }
    }
}