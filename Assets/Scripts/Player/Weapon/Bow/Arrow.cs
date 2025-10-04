using Enemy;
using GameCore.Pause;
using UnityEngine;
using Zenject;

namespace Player.Weapon.Bow
{
    public class Arrow: Projectile
    {
        private BowWeapon _bowWeapon;
        private GamePause _gamePause;

        [Inject]
        private void Construct(BowWeapon bowWeapon, GamePause gamePause)
        {
            _bowWeapon = bowWeapon;
            _gamePause = gamePause;
        }
        
        private void Update()
        {
            if (_gamePause.IsStopped) return;
            transform.position += transform.up * (-1 * _bowWeapon.Speed * Time.deltaTime);
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            Timer = new WaitForSeconds(_bowWeapon.Duration);
            Damage = _bowWeapon.ChargedDamage;
        }

        protected override void OnTriggerEnter2D(Collider2D other)
        {
            base.OnTriggerEnter2D(other);
            if (_bowWeapon.CurrentLevel <= 4 && other.TryGetComponent(out EnemyHealth enemyHealth))
            {
                gameObject.SetActive(false);
            }
        }
    }
}