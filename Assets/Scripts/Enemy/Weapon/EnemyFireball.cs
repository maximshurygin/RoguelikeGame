using Player;
using Player.Weapon;
using UnityEngine;

namespace Enemy.Weapon
{
    public class EnemyFireball : Projectile
    {
        [SerializeField] private Transform _sprite;

        private float _speed;

        public void Init(float duration, float speed, float damage)
        {
            Timer  = new WaitForSeconds(duration);
            Damage = damage;
            _speed = speed;
        }

        protected override void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out PlayerHealth playerHealth))
            {
                playerHealth.TakeDamage(Damage);
                gameObject.SetActive(false);
            }

        }

        private void Update()
        {
            transform.position += transform.right * (_speed * Time.deltaTime);
        }
    }
}