using GameCore.GameObjectPool;
using UnityEngine;
using Zenject;

namespace GameCore.ParticleSystem
{
    public class ParticleSpawner : MonoBehaviour
    {
        [SerializeField] protected ObjectPool _objectPool;
        private Transform _particleContainer;

        [Inject]
        private void Construct(Transform particleContainer)
        {
            _particleContainer = particleContainer;
        }
        public virtual void Spawn()
        {
            var experienceObject = _objectPool.GetFromPool();
            experienceObject.transform.SetParent(_particleContainer, true);
            experienceObject.transform.position = transform.position;
        }
    }
}