using System.Collections;
using GameCore.ExperienceSystem;
using GameCore.Health;
using GameCore.UI;
using Player;
using UnityEngine;
using Zenject;

namespace Enemy
{
    enum EnemyType
    {
        Easy,
        Medium,
        Hard,
        Boss
    }
    public class EnemyHealth: ObjectHealth
    {
        [SerializeField] private EnemyType _enemyType;
        [SerializeField] private ParticleSystem _deathParticleSystem;
        private PlayerHealth _playerHealth;
        private DamageTextSpawner _damageHealthSpawner;
        private WaitForSeconds _tick = new WaitForSeconds(1f);
        private ExperienceSpawner _experienceSpawner;
        private float _chanceToDropExp = 33f;
        private float _experienceToDrop = 3f;

        [Inject]
        private void Construct(ExperienceSpawner experienceSpawner, PlayerHealth playerHealth, DamageTextSpawner damageHealthSpawner)
        {
            _experienceSpawner = experienceSpawner;
            _playerHealth = playerHealth;
            _damageHealthSpawner = damageHealthSpawner;
        }
        
        public override void TakeDamage(float damage)
        {
            base.TakeDamage(damage);
            _damageHealthSpawner.Activate(transform, (int)damage);
            if (CurrentHealth <= 0)
            {
                Instantiate(_deathParticleSystem, transform.position, Quaternion.identity);
                gameObject.SetActive(false);
                ChanceToDropExperience();
                
                if (_enemyType == EnemyType.Boss)
                {
                    _playerHealth.TakeHeal(float.MaxValue);
                }
            }
        }

        private void ChanceToDropExperience()
        {
            switch (_enemyType)
            {
                case EnemyType.Easy:
                    _chanceToDropExp = 33f;
                    _experienceToDrop = 3f;
                    break;
                case EnemyType.Medium:
                    _chanceToDropExp = 50f;
                    _experienceToDrop = 5f;
                    break;
                case EnemyType.Hard:
                    _chanceToDropExp = 66f;
                    _experienceToDrop = 7f;
                    break;
                case EnemyType.Boss:
                    _chanceToDropExp = 100f;
                    _experienceToDrop = 50f;
                    break;
            }

            if (Random.Range(0f, 100f) <= _chanceToDropExp)
            {
                Vector3 randomSpawnLocation = transform.position +
                                              new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), 0f);
                _experienceSpawner.Spawn(randomSpawnLocation, _experienceToDrop);
            }
        }

        public void Burn(float damage)
        {
            StartCoroutine(StartBurn(damage));
        }

        private IEnumerator StartBurn(float damage)
        {
            if (gameObject.activeSelf == false)
            {
                yield break;
            }
            
            float tickDamage = damage / 3f;
            
            if (tickDamage < 1f)
            { 
                tickDamage = 1f;
            }

            float roundDamage = Mathf.Round(tickDamage);

            for (int i = 0; i < 5; i++)
            {
                TakeDamage(roundDamage);
                yield return _tick;
            }
        }
    }
}