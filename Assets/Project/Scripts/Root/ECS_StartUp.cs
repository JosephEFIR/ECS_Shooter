using Leopotam.Ecs;
using Project.Scripts.Move;
using UnityEngine;
using Voody.UniLeo;

namespace Project.Scripts.Common
{
    sealed class ECS_StartUp : MonoBehaviour
    {
        private EcsWorld _world;
        private EcsSystems _systems;

        private void Start()
        {
            _world = new EcsWorld();
            _systems = new EcsSystems(_world);

            _systems.ConvertScene();
            
            AddInjections();
            AddOneFrames();
            AddSystems();
            
            _systems.Init();
        }

        private void Update()
        {
            _systems?.Run();
        }

        private void AddOneFrames()
        {
            // хз че это
        }

        private void AddInjections()
        {
            // а Zenject???
        }

        private void AddSystems() // А если их дохера будет?
        {
            PlayerSystems();
        }

        private void PlayerSystems()
        {
            _systems
                .Add(new PlayerInputSystem())
                .Add(new PlayerMovementSystem())
                .Add(new PlayerMouseInputSystem())
                .Add(new PlayerMouseLookSystem())
                ;
        }

        private void OnDestroy()
        {
            if(_systems is null) return;
            
            _systems.Destroy();
            _systems = null;
            
            _world.Destroy();
            _world = null;
        }
    }
}