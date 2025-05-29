using Leopotam.Ecs;
using Project.Scripts.Animation;
using Project.Scripts.Common;
using Project.Scripts.Move;
using Project.Scripts.Tags;

namespace Project.Scripts.Player
{
    public class PlayerPreInitSystem : IEcsPreInitSystem
    {
        private readonly EcsFilter<PlayerComponent> _playerFilter = null;
        public void PreInit()
        {
            foreach (var entity in _playerFilter)
            {
                EcsEntity playerEntity = _playerFilter.GetEntity(entity);
                
                ref var playerComponent = ref playerEntity.Get<PlayerComponent>();
                
                playerEntity.Get<PlayerAnimationComponent>().Animator = playerComponent.Animator;
                
                ref var cameraSwitchComponent = ref playerEntity.Get<CameraSwitcherComponent>();
                cameraSwitchComponent.firstPersonViewCam = playerComponent.FPVCamera;
                cameraSwitchComponent.thirdPersonViewCam = playerComponent.TPVCamera;
            }
        }
    }
}