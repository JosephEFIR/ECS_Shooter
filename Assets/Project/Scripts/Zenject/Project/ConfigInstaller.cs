using Project.Scripts.Configs;
using Project.Scripts.Configs.Spawn;
using UnityEngine;
using Zenject;

namespace Project.Scripts.Zenject.Project
{
    public class ConfigInstaller : MonoInstaller
    {
        [SerializeField] private SpawnConfig spawnConfig;
        [SerializeField] private SoundEffectConfig soundConfig;
        
        public override void InstallBindings()
        {
            Container.BindInstance(spawnConfig).AsSingle();
            Container.BindInstance(soundConfig).AsSingle();
        }
    }
}