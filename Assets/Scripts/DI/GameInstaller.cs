using GameCore;
using GameCore.GameObjectPool;
using UnityEngine;
using Zenject;

namespace DI
{
    public class GameInstaller: MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<GetRandomSpawnPoint>().FromNew().AsSingle().NonLazy();
        }
    }
}