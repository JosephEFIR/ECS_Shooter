using Leopotam.Ecs;
using Project.Scripts.Animation;
using Project.Scripts.Move;
using Project.Scripts.Other;
using Project.Scripts.Player;
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
            _systems.OneFrame<JumpEvent>();
            _systems.OneFrame<CameraSwitchEvent>();
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
                .Add(new PlayerPreInitSystem())
                    
                .Add(new PlayerJumpSendEventSystem())
                .Add(new CameraSwitcherSendEventSystem())
                
                .Add(new PlayerGroundCheckSystem())
                .Add(new PlayerInputSystem())
                .Add(new PlayerMovementSystem())
                .Add(new PlayerMouseInputSystem())
                .Add(new PlayerMouseLookSystem())
                .Add(new PlayerJumpSystem())
                .Add(new PlayerAnimationSystem())
                .Add(new CameraSwitcherSystem())
                .Add(new CursorLockedSystem())
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