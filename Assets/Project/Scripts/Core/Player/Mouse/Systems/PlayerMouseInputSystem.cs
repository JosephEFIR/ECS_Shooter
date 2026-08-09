using Leopotam.Ecs;
using Project.Scripts.Tags;
using Project.Scripts.Weapon;
using UnityEngine;

namespace Project.Scripts.Move
{
    sealed class PlayerMouseInputSystem : IEcsRunSystem
    {
        private readonly EcsFilter<PlayerComponent,MouseLookComponent> _playerFilter = null;

        private float _axisX;
        private float _axisY;
        
        public void Run()
        {
            GetAxis();
            ClampAxis();

            foreach (var i in _playerFilter)
            {
                ref var entity = ref _playerFilter.GetEntity(i);
                ref var lookComponent = ref _playerFilter.Get2(i);
                
                lookComponent.Direction.x = _axisX;
                lookComponent.Direction.y = _axisY;

                if(Input.GetKey(KeyCode.Mouse1)) entity.Get<AimComponent>();
                if(Input.GetKeyUp(KeyCode.Mouse1)) entity.Del<AimComponent>();
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