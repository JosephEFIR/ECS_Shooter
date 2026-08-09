using Project.Scripts.Configs.Spawn;
using UnityEngine;
using Zenject;

namespace Project.Scripts.Zenject.Project
{
    public class ConfigInstaller : MonoInstaller
    {
        [SerializeField] private SpawnConfig spawnConfig;
        
        public override void InstallBindings()
        {
            Container.BindInstance(spawnConfig).AsSingle();
        }
    }
}