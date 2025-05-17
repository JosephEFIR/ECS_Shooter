using System;
using Leopotam.Ecs;
using Project.Scripts.Tags;

namespace Project.Scripts.Move
{
    sealed class PlayerJumpSystem : IEcsRunSystem
    {
        private readonly EcsFilter<PlayerMovableComponent,JumpEvent> _jumpFilter = null;
        
        public void Run()
        {
            foreach (var i in _jumpFilter)
            {
                ref var movableComponent = ref _jumpFilter.Get1(i);
                ref var velocity = ref movableComponent.Velocity;
                ref var grounded = ref movableComponent.IsGrounded;
                
                if(!grounded) continue;

                velocity.y = MathF.Sqrt(movableComponent.JumpForce * -2F * movableComponent.Gravity);
            }
        }
    }
}