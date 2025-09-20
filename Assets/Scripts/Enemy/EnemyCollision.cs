using GameCore.GameObjectPool;
using Player;
using UnityEngine;
using Zenject;

namespace Enemy
{
    public class EnemyCollision : MonoBehaviour
    {
        [SerializeField] private float damage;
        private GameObjectPool _enemyPool;
        
        [Inject]
        public void Construct(GameObjectPool enemyPool)
        {
            _enemyPool = enemyPool;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent(out PlayerHealth player))
            {
                player.TakeDamage(damage);
                _enemyPool.Release(gameObject);
            }
        }
    }
}