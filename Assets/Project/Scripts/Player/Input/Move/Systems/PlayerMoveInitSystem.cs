using Leopotam.Ecs;
using Project.Scripts.Common;
using Project.Scripts.Tags;

namespace Project.Scripts.Move
{
    public class PlayerMoveInitSystem : IEcsInitSystem
    {
        private readonly EcsFilter<PlayerComponent> _playerFilter = null;
        public void Init()
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
                movableComponent.JumpForce = playerComponent.Config.JumpPower;
                movableComponent.GroundCheckTransform = playerComponent.GroundCheck;
                movableComponent.GroundDistance = playerComponent.Config.GroundDistance;
                movableComponent.GroundLayer = playerComponent.Config.GroundLayer;
                
                playerEntity.Get<DirectionComponent>();
                playerEntity.Get<ModelComponent>().ModelTransform = playerComponent.Position;
                
                ref var mouseComponent = ref playerEntity.Get<MouseLookComponent>();
                mouseComponent.Sensitivity = playerComponent.Config.MouseSensitivity;
                mouseComponent.Camera = playerComponent.camera;
            }
        }
    }
}