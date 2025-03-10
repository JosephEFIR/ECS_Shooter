using Leopotam.Ecs;
using Project.Scripts.Tags;
using UnityEngine;

namespace Project.Scripts.Move
{
    sealed class PlayerMouseInputSystem : IEcsRunSystem
    {
        private readonly EcsFilter<PlayerTag,MouseLookComponent> _playerFilter = null;

        private float _axisX;
        private float _axisY;
        
        public void Run()
        {
            GetAxis();
            ClampAxis();

            foreach (var i in _playerFilter)
            {
                ref var lookComponent = ref _playerFilter.Get2(i);
                
                lookComponent.Direction.x = _axisX;
                lookComponent.Direction.y = _axisY;
                
            }
        }

        private void GetAxis()
        {
            _axisX += Input.GetAxis("Mouse X");
            _axisY -= Input.GetAxis("Mouse Y");
        }

        private void ClampAxis()
        {
            _axisY = Mathf.Clamp(_axisY, -86, 75);
        }
    }
}