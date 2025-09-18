using System.Collections;
using GameCore.GameObjectPool;
using GameCore.Health;
using UnityEngine;
using Zenject;

namespace Enemy
{
    public class EnemyHealth: ObjectHealth
    {
        private GameObjectPool _enemyPool;
        private WaitForSeconds _tick = new WaitForSeconds(1f);

        [Inject]
        public void Construct(GameObjectPool enemyPool)
        {
            _enemyPool = enemyPool;
        }
        
        public override void TakeDamage(float damage)
        {   
            base.TakeDamage(damage);
            if (CurrentHealth <= 0 && gameObject.activeInHierarchy)
            {
                _enemyPool.Release(gameObject);
            }
        }
        public void Burn(float damage) => StartCoroutine(StartBurn(damage));

        private IEnumerator StartBurn(float damage)
        {
            if (!enabled)
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