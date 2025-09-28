using System.Collections;
using GameCore.ExperienceSystem;
using GameCore.Health;
using UnityEngine;
using Zenject;

namespace Enemy
{
    enum EnemyType
    {
        Easy,
        Medium,
        Hard
    }
    public class EnemyHealth: ObjectHealth
    {
        [SerializeField] private EnemyType _enemyType;
        private WaitForSeconds _tick = new WaitForSeconds(1f);
        private ExperienceSpawner _experienceSpawner;
        private float _chanceToDropExp = 33f;
        private float _experienceToDrop = 3f;

        [Inject]
        private void Construct(ExperienceSpawner experienceSpawner)
        {
            _experienceSpawner = experienceSpawner;
        }
        
        public override void TakeDamage(float damage)
        {
            base.TakeDamage(damage);
            if (CurrentHealth <= 0)
            {
                gameObject.SetActive(false);
                ChanceToDropExperience();
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