using System.Collections;
using UnityEngine;

namespace Enemy
{
    public class BossSpawner : EnemySpawner
    {
        private Coroutine _spawnBossCoroutine;
        public override void Activate()
        {
            SpawnBoss();
        }

        public override void Deactivate()
        {
            
        }

        private void SpawnBoss()
        {
            transform.position = PlayerMovement.transform.position;
            GameObject enemy = EnemyPool.GetFromPool();
            enemy.transform.SetParent(EnemyContainer);
            enemy.transform.position = SpawnPoint.GetRandomPoint(MinPos, MaxPos);
        }
    }
}