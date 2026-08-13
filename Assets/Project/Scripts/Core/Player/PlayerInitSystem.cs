using Leopotam.Ecs;
using Project.Scripts.Animation;
using Project.Scripts.Common;
using Project.Scripts.Core.Common;
using Project.Scripts.Core.Player;
using Project.Scripts.Tags;
using Project.Scripts.Weapon;
using UnityEngine;

namespace Project.Scripts.Move
{
    public class PlayerInitSystem : IEcsRunSystem
    {
        private readonly EcsFilter<PlayerComponent, InitComponent> _playerFilter = null;
        public void Run()
        {
            foreach (var entity in _playerFilter)
            {
                EcsEntity playerEntity = _playerFilter.GetEntity(entity);
                
                ref var playerComponent = ref playerEntity.Get<PlayerComponent>();
                
                //Move
                ref var movableComponent = ref playerEntity.Get<PlayerMovableComponent>();
                movableComponent.Rigidbody = playerComponent.Rigidbody;
                movableComponent.Speed = playerComponent.Config.Speed;
                movableComponent.RunSpeed = playerComponent.Config.RunSpeed;
                movableComponent.JumpForce = playerComponent.Config.JumpPower;
                movableComponent.GroundChecker = playerComponent.GroundChecker;
                movableComponent.Collider  = playerComponent.Collider;
                movableComponent.CrouchChecker = playerComponent.CrouchChecker;
                
                //Base
                playerEntity.Get<DirectionComponent>();
                ref var model = ref playerEntity.Get<ModelComponent>();
                model.ModelTransform = playerComponent.Position;
                model.StartRotation = model.ModelTransform.rotation; //ЭТО ЧТО БРУХ
                
                
                //Mouse
                ref var mouseComponent = ref playerEntity.Get<MouseLookComponent>();
                mouseComponent.Sensitivity = playerComponent.Config.MouseSensitivity;
                mouseComponent.Camera = playerComponent.camera;
                
                //Animation
                ref var animComponent = ref playerEntity.Get<PlayerAnimationComponent>();
                animComponent.Animator = playerComponent.Animator;
                animComponent.ElbowIKAmount = playerComponent.Config.ElbowIKAmount;
                animComponent.HandIKAmount = playerComponent.Config.HandIKAmount;
                
                //Cameras
                ref var cameraSwitchComponent = ref playerEntity.Get<CamerasComponent>();
                cameraSwitchComponent.isFPV = true;
                cameraSwitchComponent.firstPersonViewCam = playerComponent.FPVCamera;
                cameraSwitchComponent.thirdPersonViewCam = playerComponent.TPVCamera;
                
                playerEntity.Get<WeaponInventoryComponent>();
                playerEntity.Get<TakeWeaponEvent>();

                playerEntity.Get<InitializedEvent>();
                if(playerEntity.Has<InitComponent>()) playerEntity.Del<InitComponent>();
                
                Debug.Log("Player Initialized");
            }
        }
    }
}