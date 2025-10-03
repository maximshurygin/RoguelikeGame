using GameCore.ParticleSystem;
using Player;
using Player.Weapon;
using Player.Weapon.Bow;
using Player.Weapon.FrostBoltWeapon;
using Player.Weapon.Suriken;
using Player.Weapon.Trap;
using UnityEngine;
using Zenject;

namespace GameCore.UpgradeSystem
{
    public class PlayerUpgrade : MonoBehaviour
    {
        [SerializeField] private WeaponUpgradeParticleSpawner _weaponUpgradeParticleSpawner;
        [SerializeField] private PlayerUpgradeParticleSpawner _playerUpgradeParticleSpawner;

        
        private PlayerHealth _playerHealth;
        private PlayerMovement _playerMovement;

        private FireBallWeapon _fireBallWeapon;
        private AuraWeapon _auraWeapon;
        private SurikenWeapon _surikenWeapon;
        private FrostBoltWeapon _frostBoltWeapon;
        private TrapWeapon _trapWeapon;
        private BowWeapon _bowWeapon;
        private SwordWeapon _swordWeapon;

        public FireBallWeapon FireBallWeapon => _fireBallWeapon;
        public AuraWeapon AuraWeapon => _auraWeapon;
        public SurikenWeapon SurikenWeapon => _surikenWeapon;
        public FrostBoltWeapon FrostBoltWeapon => _frostBoltWeapon;
        public TrapWeapon TrapWeapon => _trapWeapon;
        public BowWeapon BowWeapon => _bowWeapon;
        public SwordWeapon SwordWeapon => _swordWeapon;
        
        public float RangeExp { get; private set; }
        
        [Inject]
        private void Construct(PlayerHealth playerHealth, PlayerMovement playerMovement, FireBallWeapon fireBallWeapon,
            AuraWeapon auraWeapon, SurikenWeapon surikenWeapon, FrostBoltWeapon frostBoltWeapon, TrapWeapon trapWeapon,
            BowWeapon bowWeapon, SwordWeapon swordWeapon)
        {
            _playerHealth = playerHealth;
            _playerMovement = playerMovement;
            _fireBallWeapon = fireBallWeapon;
            _auraWeapon = auraWeapon;
            _surikenWeapon = surikenWeapon;
            _frostBoltWeapon = frostBoltWeapon;
            _trapWeapon = trapWeapon;
            _bowWeapon = bowWeapon;
            _swordWeapon = swordWeapon;
        }

        private void Start()
        {
            RangeExp = 1.5f;
        }

        public void UpgradeHealth()
        {
            _playerHealth.UpgradeHealth();
            SpawnPlayerUpgradeParticles();
        }

        public void UpgradeRegeneration()
        {
            _playerHealth.UpgradeRegeneration();
            SpawnPlayerUpgradeParticles();
        }

        public void UpgradeSpeed()
        {
            _playerMovement.UpgradeSpeed();
            SpawnPlayerUpgradeParticles();
        }

        public void UpgradeRangeExp()
        {
            RangeExp++;
            SpawnPlayerUpgradeParticles();
        }

        public void UpgradeWeapon(BaseWeapon weapon)
        {
            SpawnWeaponUpgradeParticles();
            if (weapon.gameObject.activeSelf)
            {
                weapon.LevelUp();
            }
            else
            {
                weapon.gameObject.SetActive(true);
            }
        }

        private void SpawnPlayerUpgradeParticles()
        {
            _playerUpgradeParticleSpawner.Spawn();
        }
        
        private void SpawnWeaponUpgradeParticles()
        {
            _weaponUpgradeParticleSpawner.Spawn();
        }
    }
}