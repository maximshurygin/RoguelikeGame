using GameCore;
using GameCore.ExperienceSystem;
using GameCore.LevelSystem;
using GameCore.Loot;
using GameCore.Pause;
using GameCore.UI;
using GameCore.UpgradeSystem;
using UnityEngine;
using Zenject;

namespace DI
{
    public class GameInstaller: MonoInstaller
    {
        [SerializeField] private LevelSystem _levelSystem;
        [SerializeField] private GameTimer _gameTimer;
        [SerializeField] private ExperienceSystem _experienceSystem;
        [SerializeField] private PlayerUpgrade _playerUpgrade;
        [SerializeField] private ExperienceSpawner _experienceSpawner;
        [SerializeField] private DamageTextSpawner _damageTextSpawner;
        [SerializeField] private Transform _particleContainer;
        [SerializeField] private UpgradeWindow _upgradeWindow;
        [SerializeField] private GamePause _gamePause;
        [SerializeField] private CoinsUIUpdater _coinsUIUpdater;
        [SerializeField] private TreasureWindow _treasureWindow;
        [SerializeField] private RewardCoinsAnimation _rewardCoinsAnimation;
        [SerializeField] private BombSpawner _bombSpawner;

        


        public override void InstallBindings()
        {
            Container.Bind<GetRandomSpawnPoint>().FromNew().AsSingle().NonLazy();
            Container.Bind<LevelSystem>().FromInstance(_levelSystem).AsSingle().NonLazy();
            Container.Bind<GameTimer>().FromInstance(_gameTimer).AsSingle().NonLazy();
            Container.Bind<ExperienceSystem>().FromInstance(_experienceSystem).AsSingle().NonLazy();
            Container.Bind<PlayerUpgrade>().FromInstance(_playerUpgrade).AsSingle().NonLazy();
            Container.Bind<ExperienceSpawner>().FromInstance(_experienceSpawner).AsSingle().NonLazy();
            Container.Bind<DamageTextSpawner>().FromInstance(_damageTextSpawner).AsSingle().NonLazy();
            Container.Bind<Transform>().FromInstance(_particleContainer).AsSingle().NonLazy();
            Container.Bind<UpgradeWindow>().FromInstance(_upgradeWindow).AsSingle().NonLazy();
            Container.Bind<GamePause>().FromInstance(_gamePause).AsSingle().NonLazy();
            Container.Bind<CoinsUIUpdater>().FromInstance(_coinsUIUpdater).AsSingle().NonLazy();
            Container.Bind<CoinKeeper>().FromNew().AsSingle().NonLazy();
            Container.Bind<TreasureWindow>().FromInstance(_treasureWindow).AsSingle().NonLazy();
            Container.Bind<RewardCoinsAnimation>().FromInstance(_rewardCoinsAnimation).AsSingle().NonLazy();
            Container.Bind<BombSpawner>().FromInstance(_bombSpawner).AsSingle().NonLazy();

        }
    }
}