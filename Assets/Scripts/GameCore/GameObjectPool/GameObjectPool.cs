using UnityEngine;
using UnityEngine.Pool;
using Zenject;

namespace GameCore.GameObjectPool
{
    public class GameObjectPool : MonoBehaviour
    {
        [SerializeField] private GameObject[] prefabs;
        private ObjectPool<GameObject> _gameObjectPool;
        private DiContainer _diContainer;
        
        [Inject]
        private void Construct(DiContainer diContainer)
        {
            _diContainer = diContainer;
            CreateGameObjectPool();
        }
        
        private void CreateGameObjectPool()
        {
            _gameObjectPool = new ObjectPool<GameObject>(() =>
            {
                return _diContainer.InstantiatePrefab(prefabs[Random.Range(0, prefabs.Length)]);
            }, currentGameObject =>
            {
                currentGameObject.SetActive(true);
            }, currentGameObject =>
            {
                currentGameObject.SetActive(false);
            }, currentGameObject =>
            {
                Destroy(currentGameObject);
            });
        }
        
        
        public void Release(GameObject currentGameObject)
        {
            _gameObjectPool.Release(currentGameObject);
        }
        
        public GameObject Get()
        {
            return _gameObjectPool.Get();
        }
    }
}