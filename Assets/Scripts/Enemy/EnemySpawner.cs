using System.Collections;
using GameCore;
using GameCore.GameObjectPool;
using Player;
using UnityEngine;
using Zenject;

namespace Enemy
{
    public class EnemySpawner: MonoBehaviour
    {
        [SerializeField] private float timeToSpawn;
        [SerializeField] private Transform minPos, maxPos;
        [SerializeField] private Transform enemyContainer;
        private GameObjectPool _enemyPool;
        private PlayerMovement _playerMovement;
        private WaitForSeconds _interval;
        private GetRandomSpawnPoint _spawnPoint;
        private Coroutine _spawnCoroutine;

        [Inject]
        public void Construct(GetRandomSpawnPoint getRandomSpawnPoint, PlayerMovement playerMovement, GameObjectPool enemyPool)
        {
            _spawnPoint = getRandomSpawnPoint;
            _playerMovement = playerMovement;
            _enemyPool = enemyPool;
        }

        private void Start()
        {
            _interval = new WaitForSeconds(timeToSpawn);
            Activate();
        }

        public void Activate()
        {
            _spawnCoroutine = StartCoroutine(Spawn());
        }

        public void Deactivate()
        {
            if (_spawnCoroutine != null)
            {
                StopCoroutine(_spawnCoroutine);
            }
        }

        public void OnDisable()
        {
            Deactivate();
        }

        private IEnumerator Spawn()
        {
            while (true)
            {
                transform.position = _playerMovement.transform.position;
                GameObject enemy = _enemyPool.Get();
                enemy.transform.position = _spawnPoint.GetRandomPoint(minPos, maxPos);
                enemy.transform.SetParent(enemyContainer);
                yield return _interval;
            }
        }
    }
}