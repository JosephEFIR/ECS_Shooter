using Leopotam.Ecs;
using Project.Scripts.Common;
using UnityEngine;

namespace Project.Scripts.Move
{
    sealed class PlayerMovementSystem : IEcsRunSystem
    {
        private readonly EcsFilter<ModelComponent,PlayerMovableComponent> _movableFilter = null;
        
        public void Run()
        {
            foreach (var variable in _movableFilter)
            {
                ref var modelComponent = ref _movableFilter.Get1(variable);
                ref var movableComponent = ref _movableFilter.Get2(variable);
                
                ref var transform = ref modelComponent.ModelTransform;

                ref var rigidbody = ref movableComponent.Rigidbody;
                ref var walkSpeed = ref movableComponent.Speed;
                ref var runSpeed = ref movableComponent.RunSpeed;
                
                float moveHorizontal = Input.GetAxis("Horizontal");
                float moveVertical = Input.GetAxis("Vertical");

                
                Vector3 movement = transform.forward * moveVertical + transform.right * moveHorizontal;
                movement.y = 0f;
                
                if (movement.magnitude > 1f)
                    movement.Normalize();
                
                float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;
                movableComponent.IsRun = Input.GetKey(KeyCode.LeftShift);
                
                Vector3 targetVelocity = movement * currentSpeed;
                
                targetVelocity.y = rigidbody.linearVelocity.y;
                rigidbody.linearVelocity = targetVelocity;
            }
        }
    }
}