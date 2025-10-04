using Enemy;
using GameCore.Pause;
using UnityEngine;
using Zenject;

namespace Player.Weapon.FrostBoltWeapon
{
    public class FrostBolt : Projectile
    {
        private FrostBoltWeapon _frostBoltWeapon;
        private GamePause _gamePause;

        [Inject]
        private void Constuct(FrostBoltWeapon frostBoltWeapon, GamePause gamePause)
        {
            _frostBoltWeapon = frostBoltWeapon;
            _gamePause = gamePause;
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            Timer = new WaitForSeconds(_frostBoltWeapon.Duration);
            Damage = _frostBoltWeapon.Damage;
        }

        private void Update()
        {
            if (_gamePause.IsStopped) return;
            transform.position += transform.right * (_frostBoltWeapon.Speed * Time.deltaTime);
        }

        protected override void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out EnemyHealth enemy))
            {
                enemy.TakeDamage(Damage);
                float slowdownRate = _frostBoltWeapon.SlowdownRate;
                WaitForSeconds slowdownDuration = _frostBoltWeapon.SlowdownDuration;
                if (_frostBoltWeapon.CurrentLevel >= 5 && Random.Range(0f, 1f) < 0.5f)
                {
                    slowdownRate = float.MaxValue;
                    slowdownDuration = new WaitForSeconds(2f);
                }
                enemy.GetComponent<EnemyMove>()?.SlowDown(slowdownRate, slowdownDuration);
            }

            if (_frostBoltWeapon.CurrentLevel <= 4 && other.TryGetComponent(out EnemyHealth enemyHealth))
            {
                gameObject.SetActive(false);
            }
        }
    }
}