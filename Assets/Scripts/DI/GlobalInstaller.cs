using GameCore.Save;
using GameCore.ScenesLoader;
using Menu.Shop;
using Player;
using UnityEngine;
using Zenject;

namespace DI
{
    public class GlobalInstaller: MonoInstaller
    {
        [SerializeField] private UpgradeLoader _upgradeLoader;

        public override void InstallBindings()
        {
            Container.Bind<PlayerData>().FromNew().AsSingle().NonLazy();
            Container.Bind<SaveProgress>().FromNew().AsSingle().NonLazy();
            Container.Bind<SceneLoader>().FromNew().AsSingle().NonLazy();
            Container.Bind<UpgradeLoader>().FromInstance(_upgradeLoader);

        }
    }
}