using System;
using Leopotam.Ecs;
using Project.Scripts.Tags;
using UnityEngine;

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
                ref var grounded = ref movableComponent.IsGrounded;
                
                if(!grounded) continue;

                float jumpForce = movableComponent.JumpForce;
                movableComponent.Rigidbody.linearVelocity= new Vector2(movableComponent.Rigidbody.linearVelocity.x,jumpForce);
            }
        }
    }
}