using Player;
using UnityEngine;

namespace GameCore.Loot
{
    public class Loot : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out PlayerHealth player))
            {
                PickUp();
            }
        }

        protected virtual void PickUp()
        {
            AudioSource.PlayClipAtPoint(_audioSource.clip, transform.position);
            gameObject.SetActive(false);
        }
    }
}