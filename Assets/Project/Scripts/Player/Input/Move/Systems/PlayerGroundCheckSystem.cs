using Leopotam.Ecs;
using Project.Scripts.Animation;
using Project.Scripts.Tags;
using UnityEngine;

namespace Project.Scripts.Move
{
    sealed class PlayerGroundCheckSystem : IEcsRunSystem
    {
        private readonly EcsFilter<PlayerMovableComponent> _groundFilter = null;
        
        public void Run()
        {
            foreach (var i in _groundFilter)
            {
                ref var groundCheck = ref _groundFilter.Get1(i);
                
                groundCheck.IsGrounded= Physics.CheckSphere(groundCheck.GroundCheckTransform.position, groundCheck.GroundDistance, groundCheck.GroundLayer);
            }
        }
    }
}