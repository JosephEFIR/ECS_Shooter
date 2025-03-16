using Leopotam.Ecs;
using Project.Scripts.Common;
using Project.Scripts.Tags;
using UnityEngine;

namespace Project.Scripts.Move
{
    sealed class PlayerMovementSystem : IEcsRunSystem
    {
        private readonly EcsWorld _world = null;
        
        private readonly EcsFilter<PlayerTag,ModelComponent,CharMovableComponent, DirectionComponent> _movableFilter = null;
        
        public void Run()
        {
            foreach (var variable in _movableFilter)
            {
                ref var modelComponent = ref _movableFilter.Get2(variable);
                ref var movableComponent = ref _movableFilter.Get3(variable);
                ref var directionComponent = ref _movableFilter.Get4(variable);
                
                ref var direction = ref directionComponent.Direction;
                ref var tranform = ref modelComponent.ModelTransform;

                ref var characterController = ref movableComponent.CharController;
                ref var speed = ref movableComponent.Speed;
                
                var rawDirection = (tranform.right * direction.x) + (tranform.forward * direction.y);

                characterController.Move(rawDirection * speed * Time.deltaTime);
            }
        }
    }
}