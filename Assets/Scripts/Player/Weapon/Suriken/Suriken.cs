using System;
using UnityEngine;
using Zenject;

namespace Player.Weapon.Suriken
{
    public class Suriken : Projectile
    {
        [SerializeField] private Transform _sprite;
        private SurikenWeapon _surikenWeapon;
        private float elapsedTime;
        private PlayerMovement  _playerMovement;

        [Inject]
        private void Construct(SurikenWeapon surikenWeapon, PlayerMovement playerMovement)
        {
            _surikenWeapon = surikenWeapon;
            _playerMovement = playerMovement;
        }
        
        protected override void OnEnable()
        {
            base.OnEnable();
            Timer = new WaitForSeconds(_surikenWeapon.Duration * 2);
            Damage = _surikenWeapon.Damage;
            elapsedTime = 0f;
        }

        private void Update()
        {
            elapsedTime += Time.deltaTime;
            _sprite.transform.Rotate(0,0,500f * Time.deltaTime);
            if (_surikenWeapon.CurrentLevel < 5)
            {
                transform.position += transform.right * (_surikenWeapon.Speed * Time.deltaTime);
            }
            else
            {
                if (elapsedTime < _surikenWeapon.Duration)
                {
                    transform.position += transform.right * (_surikenWeapon.Speed * Time.deltaTime);
                }
                else
                {
                    Vector3 directionToPlayer = (_playerMovement.transform.position - transform.position).normalized;
                    transform.position += directionToPlayer * (_surikenWeapon.Speed * Time.deltaTime);
                    if (Vector3.Distance(transform.position, _playerMovement.transform.position) < 0.5f)
                    {
                        gameObject.SetActive(false);
                    }
                }
            }
        }
    }
}