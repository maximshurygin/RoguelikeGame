using UnityEngine;

namespace Enemy
{
    public class BossSpawner : EnemySpawner
    {
        private bool _spawned;

        public override void Activate()
        {
            if (_spawned) return;
            _spawned = true;
            SpawnBoss();
        }

        public override void Deactivate()
        {
            _spawned = false;
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