using Leopotam.Ecs;
using Project.Scripts.Animation;
using Project.Scripts.Tags;
using UnityEngine;

namespace Project.Scripts.Move
{
    sealed class PlayerModelRotateSystem : IEcsRunSystem
    {
        private readonly EcsFilter<PlayerComponent,PlayerAnimationComponent,PlayerMovableComponent,CameraSwitcherComponent> _cameraFilter = null; 
        private Vector3 lastMovementDirection;
        
        public void Run()
        {
            foreach (var i in _cameraFilter)
            {
                ref var playerAnimationComponent = ref _cameraFilter.Get2(i);
                ref var movableComponent = ref _cameraFilter.Get3(i);
                ref var cameraSwitcherComponent = ref _cameraFilter.Get4(i);
            }
        }
    }
}