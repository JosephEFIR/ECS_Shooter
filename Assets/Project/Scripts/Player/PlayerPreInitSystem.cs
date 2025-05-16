using Leopotam.Ecs;
using Project.Scripts.Animation;
using Project.Scripts.Common;
using Project.Scripts.Move;
using Project.Scripts.Tags;

namespace Project.Scripts.Player
{
    public class PlayerPreInitSystem : IEcsPreInitSystem
    {
        private EcsWorld _world = null;
        private readonly EcsFilter<PlayerComponent> _playerFilter = null;
        public void PreInit()
        {
            foreach (var entity in _playerFilter)
            {
                EcsEntity playerEntity = _playerFilter.GetEntity(entity);
                
                ref var playerComponent = ref playerEntity.Get<PlayerComponent>();
                
                ref var movableComponent = ref playerEntity.Get<PlayerMovableComponent>();
                movableComponent.CharController = playerComponent.CharacterController;
                movableComponent.Speed = playerComponent.Config.Speed;
                movableComponent.RunSpeed = playerComponent.Config.RunSpeed;
                movableComponent.Gravity = playerComponent.Config.Gravity;
                
                playerEntity.Get<DirectionComponent>();
                playerEntity.Get<PlayerAnimationComponent>().Animator = playerComponent.Animator;
                playerEntity.Get<ModelComponent>().ModelTransform = playerComponent.Position;
                playerEntity.Get<JumpComponent>().Force = playerComponent.Config.JumpPower;
                
                ref var mouseComponent = ref playerEntity.Get<MouseLookComponent>();
                mouseComponent.Sensitivity = playerComponent.Config.MouseSensitivity;
                mouseComponent.Camera = playerComponent.camera;

                ref var groundCheckComponent = ref playerEntity.Get<GroundCheckSphereComponent>();
                groundCheckComponent.groundCheckSphere = playerComponent.GroundCheck;
                groundCheckComponent.groundDistance = playerComponent.Config.GroundDistance;
                groundCheckComponent.mask = playerComponent.Config.GroundLayer;
            }
        }
    }
}