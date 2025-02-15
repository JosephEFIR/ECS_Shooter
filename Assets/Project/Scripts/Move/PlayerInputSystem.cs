using Leopotam.Ecs;
using Project.Scripts.Common;
using Project.Scripts.Tags;
using UnityEngine;

namespace Project.Scripts.Move
{
    sealed class PlayerInputSystem : IEcsRunSystem
    {
        private readonly EcsFilter<PlayerTag,DirectionComponent> _directionFilter = null;

        private float _horizontalAxis;
        private float _verticalAxis;
        
        public void Run()
        {
            SetDirection();
            
            foreach (var varibale in _directionFilter)
            {
                ref var directionComponent = ref _directionFilter.Get2(varibale);
                ref var direction = ref directionComponent.Direction;
                
                direction.x = _horizontalAxis;
                direction.y = _verticalAxis;
            }
        }

        private void SetDirection()
        {
            _horizontalAxis = Input.GetAxis("Horizontal");
            _verticalAxis = Input.GetAxis("Vertical");
        }
    }
}