using Enemy;
using UnityEngine;
using Zenject;

namespace Player.Weapon.FrostBoltWeapon
{
    public class FrostBolt : Projectile
    {
        private FrostBoltWeapon _frostBoltWeapon;

        [Inject]
        private void Constuct(FrostBoltWeapon frostBoltWeapon)
        {
            _frostBoltWeapon = frostBoltWeapon;
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            Timer = new WaitForSeconds(_frostBoltWeapon.Duration);
            Damage = _frostBoltWeapon.Damage;
        }

        private void Update()
        {
            transform.position += transform.right * (_frostBoltWeapon.Speed * Time.deltaTime);
        }

        protected override void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out EnemyHealth enemy))
            {
                enemy.TakeDamage(Damage);
                enemy.GetComponent<EnemyMove>()?.SlowDown(_frostBoltWeapon.SlowdownRate, _frostBoltWeapon.SlowdownDuration);
            }

            if (_frostBoltWeapon.CurrentLevel <= 4)
            {
                gameObject.SetActive(false);
            }
        }
    }
}