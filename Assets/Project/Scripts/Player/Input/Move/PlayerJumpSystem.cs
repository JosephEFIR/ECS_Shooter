using System;
using Leopotam.Ecs;
using Project.Scripts.Tags;

namespace Project.Scripts.Move
{
    sealed class PlayerJumpSystem : IEcsRunSystem
    {
        private readonly EcsFilter<PlayerTag,GroundCheckSphereComponent,JumpComponent,JumpEvent> _jumpFilter = null;
        
        public void Run()
        {
            foreach (var i in _jumpFilter)
            {
                ref var entity = ref _jumpFilter.GetEntity(i);
                ref var groundCheck = ref _jumpFilter.Get2(i);
                ref var jumpComponent = ref _jumpFilter.Get3(i);
                ref var movable = ref entity.Get<PlayerMovableComponent>();
                ref var velocity = ref movable.velocity;
                
                if(!groundCheck.isGrounded) continue;

                velocity.y = MathF.Sqrt(jumpComponent.force * -2F * movable.gravity);
            }
        }
    }
}