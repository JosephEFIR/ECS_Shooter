using Leopotam.Ecs;
using UnityEngine;

namespace Project.Scripts.Move
{
    sealed class CameraSwitcherSendEventSystem : IEcsRunSystem
    {
        private readonly EcsFilter<CameraSwitcherComponent> _filter = null;
        
        public void Run()
        {
            if(!Input.GetKeyDown(KeyCode.C)) return;

            foreach (var i in _filter)
            {
                ref var entity = ref _filter.GetEntity(i);
                entity.Get<CameraSwitchEvent>();
            }
        }
    }
}