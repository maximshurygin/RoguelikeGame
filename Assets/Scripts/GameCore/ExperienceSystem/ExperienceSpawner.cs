using GameCore.GameObjectPool;
using UnityEngine;

namespace GameCore.ExperienceSystem
{
    public class ExperienceSpawner : MonoBehaviour
    {
        [SerializeField] private ObjectPool _objectPool;

        public void Spawn(Vector3 position, float expValue)
        {
            var experienceObject = _objectPool.GetFromPool();
            var experience = experienceObject.GetComponent<Experience>();
            if (experience == null) return;
            experience.ExpValue = expValue;
            experienceObject.transform.SetParent(transform);
            experienceObject.transform.position = position;
        }
    }
}