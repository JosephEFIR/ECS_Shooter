using Project.Scripts.Factory;
using Zenject;

namespace Project.Scripts.Zenject.Project
{
    public class FactoriesInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<PlayerFactory>().AsSingle().NonLazy();
            Container.Bind<WeaponFactory>().AsSingle().NonLazy();
        }
    }
}