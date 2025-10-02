using GameCore.GameObjectPool;
using UnityEngine;

namespace GameCore.Loot
{
    public class BombSpawner : MonoBehaviour
    {
        [SerializeField] private ObjectPool _objectPool;

        public void Spawn(Vector3 position)
        {
            var bombObject = _objectPool.GetFromPool();
            bombObject.SetActive(true);
            bombObject.transform.SetParent(transform);
            bombObject.transform.position = position;
        }
    }
}