using GameCore.GameObjectPool;
using UnityEngine;
using Zenject;

namespace GameCore
{
    public class ParticleSpawner : MonoBehaviour
    {
        [SerializeField] private ObjectPool _objectPool;
        private Transform _particleContainer;

        [Inject]
        private void Construct(Transform particleContainer)
        {
            _particleContainer = particleContainer;
        }
        public void Spawn()
        {
            var experienceObject = _objectPool.GetFromPool();
            experienceObject.transform.SetParent(_particleContainer);
            experienceObject.transform.position = transform.position;
        }
    }
}