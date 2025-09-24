using System.Collections;
using Enemy;
using UnityEngine;
using Zenject;

namespace Player.Weapon.Trap
{
    public class Trap : Projectile
    {
        [SerializeField] private CircleCollider2D _collider;
        private WaitForSeconds _checkInterval = new WaitForSeconds(3f);
        private PlayerHealth _playerHealth;
        private TrapWeapon _trapWeapon;

        [Inject]
        private void Construct(PlayerHealth playerHealth, TrapWeapon trapWeapon)
        {
            _playerHealth = playerHealth;
            _trapWeapon = trapWeapon;
        }

        protected override void OnEnable()
        {
            Damage = _trapWeapon.Damage;
            _collider.enabled = false;
            StartCoroutine(CheckDistance());
        }

        protected override void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent(out EnemyHealth enemy))
            {
                enemy.TakeDamage(Damage);
                if (enemy.gameObject.activeSelf)
                {
                    enemy.Burn(Damage);
                }
                gameObject.SetActive(false);
            }
        }

        public void ActivateTrap()
        {
            _collider.enabled = true;
        }

        private IEnumerator CheckDistance()
        {
            while (true)
            {
                float distance = Vector3.Distance(transform.position, _playerHealth.transform.position);
                if (distance > 15f)
                {
                    transform.parent.gameObject.SetActive(false);
                }
                yield return _checkInterval;
            }
        }
    }
}