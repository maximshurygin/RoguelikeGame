using Enemy;
using Player;
using UnityEngine;

namespace GameCore.Loot
{
    public class Bomb: Loot
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private CircleCollider2D _collider;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        
        private float _explosionRadius = 5f;
        private float _damage = 20f;
        private bool _isActiveted;
        private Vector3 _normalSpriteScale = new Vector3(2f, 2f, 1f);
        private Vector3 _increasedSpriteScale = new Vector3(20f, 20f, 1f);

        private void OnDisable()
        {
            _isActiveted = false;
            _animator.SetTrigger("Idle");
            _spriteRenderer.transform.localScale = _normalSpriteScale;
        }
        

        protected override void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out PlayerHealth player) && !_isActiveted)
            {
                PickUp();
            }
        }
        
        protected override void PickUp()
        {
            _animator.SetTrigger("Explode");
            AudioSource.PlayClipAtPoint(_audioSource.clip, transform.position, 0.6f);
        }
        

        private void Explode()
        {
            _isActiveted = true;
            _spriteRenderer.transform.localScale = _increasedSpriteScale;

            Collider2D[] enemiesInRange =
                Physics2D.OverlapCircleAll(transform.position, _explosionRadius, LayerMask.GetMask("Enemy"));
            foreach (Collider2D enemy in enemiesInRange)
            {
                if (enemy.TryGetComponent(out EnemyHealth enemyHealth))
                {
                    enemyHealth.TakeDamage(_damage);
                }
            }
        }

        private void OnFinishExplode()
        {
            transform.parent.gameObject.SetActive(false);
        }
    }
}