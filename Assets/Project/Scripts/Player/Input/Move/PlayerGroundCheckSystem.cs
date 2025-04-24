using Leopotam.Ecs;
using Project.Scripts.Tags;
using UnityEngine;

namespace Project.Scripts.Move
{
    sealed class PlayerGroundCheckSystem : IEcsRunSystem
    {
        private readonly EcsFilter<PlayerTag,GroundCheckSphereComponent> _groundFilter = null;
        
        public void Run()
        {
            foreach (var i in _groundFilter)
            {
                ref var groundCheck = ref _groundFilter.Get2(i);
                
                groundCheck.isGrounded = Physics.CheckSphere(groundCheck.groundCheckSphere.position, groundCheck.groundDistance, groundCheck.mask);
            }
        }
    }
}