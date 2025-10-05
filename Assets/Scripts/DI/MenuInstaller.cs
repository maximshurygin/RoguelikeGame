using Menu;
using Menu.Shop;
using UnityEngine;
using Zenject;

namespace DI
{
    public class MenuInstaller: MonoInstaller
    {
        [SerializeField] private MenuUIUpdater _menuUIUpdater;
        [SerializeField] private GameShop _gameShop;

        public override void InstallBindings()
        {
            Container.Bind<MenuUIUpdater>().FromInstance(_menuUIUpdater);
            Container.Bind<GameShop>().FromInstance(_gameShop);
        }
    }
}