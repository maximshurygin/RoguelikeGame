using System.Collections.Generic;
using GameCore.Pause;
using UnityEngine;
using Zenject;

namespace GameCore.UpgradeSystem
{
    public class UpgradeWindow : MonoBehaviour
    {
        [SerializeField] private List<CardHolder> _allCards = new List<CardHolder>();
        
        [Header("Weapon Cards")]
        [SerializeField] private CardHolder _fireball;
        [SerializeField] private CardHolder _aura;
        [SerializeField] private CardHolder _suriken;
        [SerializeField] private CardHolder _frostBolt;
        [SerializeField] private CardHolder _trap;
        [SerializeField] private CardHolder _bow;
        [SerializeField] private CardHolder _sword;
        
        private List<CardHolder> _cardsInPool = new List<CardHolder>();
        private PlayerUpgrade _playerUpgrade;
        private GamePause _gamePause;


        [Inject]
        private void Construct(PlayerUpgrade playerUpgrade, GamePause gamePause)
        {
            _playerUpgrade = playerUpgrade;
            _gamePause = gamePause;
        }

        private void Start()
        {
            _allCards.Add(_fireball);
            _allCards.Add(_aura);
            _allCards.Add(_suriken);
            _allCards.Add(_frostBolt);
            _allCards.Add(_trap);
            _allCards.Add(_bow);
            _allCards.Add(_sword);
        }

        private void OnEnable()
        {
            _gamePause.SetPause(true);
            ClearPool();
            CheckWeaponLevels();
        }

        private void OnDisable()
        {
            _gamePause.SetPause(false);
            ClearPool();
            CheckWeaponLevels();
        }
        
        public void Upgrade(int id)
        {
            switch (id)
            {
                case 1:
                    _playerUpgrade.UpgradeHealth();
                    break;
                case 2:
                    _playerUpgrade.UpgradeRegeneration();
                    break;
                case 3:
                    _playerUpgrade.UpgradeSpeed();
                    break;  
                case 4:
                    _playerUpgrade.UpgradeRangeExp();
                    break;
                case 5:
                    _playerUpgrade.UpgradeWeapon(_playerUpgrade.FireBallWeapon);
                    break;
                case 6:
                    _playerUpgrade.UpgradeWeapon(_playerUpgrade.AuraWeapon);
                    break;
                case 7:
                    _playerUpgrade.UpgradeWeapon(_playerUpgrade.SurikenWeapon);
                    break;
                case 8:
                    _playerUpgrade.UpgradeWeapon(_playerUpgrade.FrostBoltWeapon);
                    break;
                case 9:
                    _playerUpgrade.UpgradeWeapon(_playerUpgrade.TrapWeapon);
                    break;
                case 10:
                    _playerUpgrade.UpgradeWeapon(_playerUpgrade.BowWeapon);
                    break;
                case 11:
                    _playerUpgrade.UpgradeWeapon(_playerUpgrade.SwordWeapon);
                    break;
            }
        }
        
        public void GetRandomCards()
        {
            while (_cardsInPool.Count < 3)
            {
                CardHolder randomCard = RandomCard();
                if (randomCard.gameObject.activeSelf) continue;
                _cardsInPool.Add(randomCard);
                randomCard.gameObject.SetActive(true);
            }
        }

        private void ClearPool()
        {
            _cardsInPool.Clear();
            for (int i = 0; i < _allCards.Count; i++)
            {
                _allCards[i].gameObject.SetActive(false);
            }
        }
        
        private CardHolder RandomCard()
        {
            return _allCards[Random.Range(0, _allCards.Count)];
        }

        private void CheckWeaponLevels()
        {
            if (_playerUpgrade.FireBallWeapon.CurrentLevel >= 8)
            {
                _allCards.Remove(_fireball);
            }
            if (_playerUpgrade.AuraWeapon.CurrentLevel >= 8)
            {
                _allCards.Remove(_aura);
            }
            if (_playerUpgrade.SurikenWeapon.CurrentLevel >= 8)
            {
                _allCards.Remove(_suriken);
            }
            if (_playerUpgrade.FrostBoltWeapon.CurrentLevel >= 8)
            {
                _allCards.Remove(_frostBolt);
            }
            if (_playerUpgrade.TrapWeapon.CurrentLevel >= 8)
            {
                _allCards.Remove(_trap);
            }
            if (_playerUpgrade.BowWeapon.CurrentLevel >= 8)
            {
                _allCards.Remove(_bow);
            }
            if (_playerUpgrade.SwordWeapon.CurrentLevel >= 8)
            {
                _allCards.Remove(_sword);
            }
        }
        
    }
}