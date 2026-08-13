using Leopotam.Ecs;
using Project.Scripts.Common;
using Project.Scripts.UI.Weapon;
using Zenject;

namespace Project.Scripts.Zenject.Level
{
    public class LevelInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            //ECS
            Container.Bind<EcsWorld>().FromNew().AsSingle().NonLazy();
            Container.Bind<ECS_StartUp>().FromComponentInHierarchy().AsSingle().NonLazy();
            Container.Bind<UiView>().FromComponentInHierarchy().AsSingle().NonLazy();
        }
    }
}