using Player;
using UnityEngine;

namespace GameCore.Loot
{
    public class Loot : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out PlayerHealth player))
            {
                PickUp();
            }
        }

        protected virtual void PickUp()
        {
            gameObject.SetActive(false);
        }
    }
}