using GameCore.Save;
using GameCore.ScenesLoader;
using Player;
using Zenject;

namespace DI
{
    public class GlobalInstaller: MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<PlayerData>().FromNew().AsSingle().NonLazy();
            Container.Bind<SaveProgress>().FromNew().AsSingle().NonLazy();
            Container.Bind<SceneLoader>().FromNew().AsSingle().NonLazy();

        }
    }
}