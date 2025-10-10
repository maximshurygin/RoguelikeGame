using System.Collections;
using System.Collections.Generic;
using Enemy;
using GameCore;
using GameCore.Pause;
using UnityEngine;
using Zenject;

namespace Player.Weapon
{
    public class AuraWeapon : BaseWeapon, IActivate
    {
        [SerializeField] private Transform _targetContainer;
        [SerializeField] private CircleCollider2D _collider;
        private List<EnemyHealth> _enemyInZone = new List<EnemyHealth>();
        private WaitForSeconds _timeBetweenAttack;
        private WaitForSeconds _slowdownDuration;
        private Coroutine _auraCoroutine;
        private float _range;
        private float _slowdownRate;
        private GamePause _gamePause;

        [Inject]
        private void Construct(GamePause gamePause)
        {
            _gamePause = gamePause;
        }


        protected override void Start()
        {
            SetStats(0);
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
            _slowdownDuration = new WaitForSeconds(WeaponStats[CurrentLevel - 1].SlowdownDuration);
            _slowdownRate = WeaponStats[CurrentLevel - 1].SlowdownRate;
            _targetContainer.transform.localScale = Vector3.one * _range;
            _collider.radius = _range / 3f;
            _lvlText.text = CurrentLevel.ToString();
        }

        private IEnumerator CheckZone()
        {
            while (true)
            {
                while (_gamePause.IsStopped)
                {
                    yield return null;
                }
                
                if (CurrentLevel < 5)
                {
                    for (int i = 0; i < _enemyInZone.Count; i++)
                    {
                        _enemyInZone[i].TakeDamage(Damage);
                    }
                }
                else
                {
                    for (int i = 0; i < _enemyInZone.Count; i++)
                    {
                        _enemyInZone[i].TryGetComponent(out EnemyMove enemyMove);
                        _enemyInZone[i].TakeDamage(Damage);
                        enemyMove.SlowDown(_slowdownRate, _slowdownDuration);
                    } 
                }
                yield return _timeBetweenAttack;
            }
        }
    }
}