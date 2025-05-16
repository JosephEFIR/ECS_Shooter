using Leopotam.Ecs;
using Project.Scripts.Tags;
using UnityEngine;

namespace Project.Scripts.Move
{
    sealed class PlayerJumpSendEventSystem : IEcsRunSystem
    {
        private readonly EcsFilter<PlayerComponent, JumpComponent> _playerFilter = null;
        
        public void Run()
        {
            if(!Input.GetKeyDown(KeyCode.Space)) return;

            foreach (var i in _playerFilter)
            {
                ref var entity = ref _playerFilter.GetEntity(i);
                entity.Get<JumpEvent>();
            }
        }
    }
}