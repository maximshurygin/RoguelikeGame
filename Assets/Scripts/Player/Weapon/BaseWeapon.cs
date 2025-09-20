using System.Collections.Generic;
using Enemy;
using UnityEngine;
using Zenject;

namespace Player.Weapon
{
    public class BaseWeapon : MonoBehaviour
    {
        [SerializeField] private List<WeaponStats> _weaponStats = new();
        [SerializeField] private float _damage;
        
        private DiContainer _diContainer;
        private int _currentLevel = 1;
        private int _maxLevel = 8;
        private string _weaponName = "Fireball";
        
        public float Damage => _damage;
        public List<WeaponStats> WeaponStats => _weaponStats;
        public int CurrentLevel => _currentLevel;
        public int MaxLevel => _maxLevel;
        public string WeaponName => _weaponName;
        
        [Inject]
        private void Construct(DiContainer diContainer)
        {
            _diContainer = diContainer;
        }

        private void Start()
        {
            SetStats(0);
        }

        private void Awake()
        {
            _diContainer.Inject(this);
        }

        public virtual void LevelUp()
        {
            if (_currentLevel < _maxLevel)
            {
                _currentLevel++;
                SetStats(_currentLevel - 1);
            }
        }

        protected virtual void SetStats(int value)
        {
            _damage = _weaponStats[value].Damage;
        }

        protected virtual void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent(out EnemyHealth enemy))
            {
                float damage = Random.Range(_damage / 2f, _damage * 1.5f);
                enemy.TakeDamage(damage);
            }
        }
    }
}