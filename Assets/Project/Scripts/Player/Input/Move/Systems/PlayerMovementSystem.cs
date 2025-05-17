using Leopotam.Ecs;
using Project.Scripts.Animation;
using Project.Scripts.Common;
using Project.Scripts.Tags;
using UnityEngine;

namespace Project.Scripts.Move
{
    sealed class PlayerMovementSystem : IEcsRunSystem
    {
        private readonly EcsFilter<ModelComponent,PlayerMovableComponent, DirectionComponent> _movableFilter = null;
        
        public void Run()
        {
            foreach (var variable in _movableFilter)
            {
                ref var modelComponent = ref _movableFilter.Get1(variable);
                ref var movableComponent = ref _movableFilter.Get2(variable);
                ref var directionComponent = ref _movableFilter.Get3(variable);
                
                ref var direction = ref directionComponent.Direction;
                ref var transform = ref modelComponent.ModelTransform;

                ref var characterController = ref movableComponent.CharController;
                ref var speed = ref movableComponent.Speed;
                ref var runSpeed = ref movableComponent.RunSpeed;
                
                var rawDirection = (transform.right * direction.x) + (transform.forward * direction.y);
                
                ref var velocity = ref movableComponent.Velocity;
                velocity.y += movableComponent.Gravity * Time.deltaTime;
                
                characterController.Move(velocity * Time.deltaTime);
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    characterController.Move(rawDirection * runSpeed * Time.deltaTime);
                    movableComponent.IsRun = true;
                    return;
                }
                
                movableComponent.IsRun = false;
                characterController.Move(rawDirection * speed * Time.deltaTime);
            }
        }
    }
}