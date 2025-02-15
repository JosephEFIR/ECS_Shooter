using System;
using Leopotam.Ecs;
using Project.Scripts.Move;
using Unity.VisualScripting;
using UnityEngine;
using Voody.UniLeo;

namespace Project.Scripts.Common
{
    public class ECS_StartUp : MonoBehaviour
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
            
        }

        private void AddInjections()
        {
            
        }

        private void AddSystems()
        {
            _systems
                .Add(new PlayerInputSystem())
                .Add(new MovementSystem())
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
        
        internal struct MovableComponent{}
    }
}