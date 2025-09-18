using GameCore;
using GameCore.GameObjectPool;
using UnityEngine;
using Zenject;

namespace DI
{
    public class GameInstaller: MonoInstaller
    {
        [SerializeField] private GameObjectPool _gameObjectPool;
        public override void InstallBindings()
        {
            Container.Bind<GetRandomSpawnPoint>().FromNew().AsSingle().NonLazy();
            Container.Bind<GameObjectPool>().FromInstance(_gameObjectPool).AsSingle().NonLazy();
        }
    }
}