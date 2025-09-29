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

        public Transform MinPos => minPos;
        public Transform MaxPos => maxPos;

        [SerializeField] private Transform enemyContainer;
        [SerializeField] private ObjectPool _enemyPool;
        
        public Transform EnemyContainer => enemyContainer;
        public ObjectPool EnemyPool => _enemyPool;
        private PlayerMovement _playerMovement;
        private WaitForSeconds _interval;
        private GetRandomSpawnPoint _spawnPoint;
        private Coroutine _spawnCoroutine;

        public PlayerMovement PlayerMovement => _playerMovement;
        public WaitForSeconds Interval => _interval;
        public GetRandomSpawnPoint SpawnPoint => _spawnPoint;

        [Inject]
        public void Construct(GetRandomSpawnPoint getRandomSpawnPoint, PlayerMovement playerMovement)
        {
            _spawnPoint = getRandomSpawnPoint;
            _playerMovement = playerMovement;
        }
        
        private void Start()
        {
            _interval = new WaitForSeconds(timeToSpawn);
        }

        public virtual void Activate()
        {
            _spawnCoroutine = StartCoroutine(Spawn());
        }

        public virtual void Deactivate()
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
                GameObject enemy = _enemyPool.GetFromPool();
                enemy.transform.SetParent(enemyContainer);
                enemy.transform.position = _spawnPoint.GetRandomPoint(minPos, maxPos);
                yield return _interval;
            }
        }
    }
}