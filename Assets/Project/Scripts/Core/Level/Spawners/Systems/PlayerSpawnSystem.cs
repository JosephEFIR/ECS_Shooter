using Leopotam.Ecs;
using Project.Scripts.Configs.Spawn;
using Project.Scripts.Core.Common;
using Project.Scripts.Factory;
using Project.Scripts.Level.Spawners;
using Project.Scripts.Player;
using Project.Scripts.Tags;
using UnityEngine;

namespace Project.Scripts.Core.Level.Spawners.Systems
{
    sealed class PlayerSpawnSystem : IEcsRunSystem
    {
        private readonly PlayerFactory _factory = null;
        private readonly SpawnConfig _spawnConfig = null;

        private readonly EcsFilter<PlayerComponent,SpawnComponent> _filter = null;
        
        
        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var entity = ref _filter.GetEntity(i);
                ref var spawnComponent = ref _filter.Get2(i);
                CreatePlayer( entity,spawnComponent.Position, spawnComponent.Parent);

                entity.Get<InitComponent>();
                entity.Del<SpawnComponent>();
            }
        }
        
        private void CreatePlayer(EcsEntity playerEntity,Transform playerTransform, Transform parent = null)
        {
            if (parent is null)
            {
                PlayerView player = _factory.Create(_spawnConfig.PlayerPrefab, playerTransform.position);
                
                PlayerComponentInit(playerEntity, player);
                
            }
            else
            { 
                PlayerView player = _factory.Create(_spawnConfig.PlayerPrefab, playerTransform.position, parent);
                
                PlayerComponentInit(playerEntity, player);
            }
        }

        private void PlayerComponentInit(EcsEntity playerEntity, PlayerView player)//TODO Какаято херня
        {
            ref var playerComponent = ref playerEntity.Get<PlayerComponent>();
            playerComponent.Config = player.Config;
            playerComponent.camera = player.Camera;
            playerComponent.FPVCamera = player.FPVCamera;
            playerComponent.TPVCamera = player.TPVCamera;
            playerComponent.Rigidbody = player.Rigidbody;
            playerComponent.Animator = player.Animator;
            playerComponent.Position = player.Position;
            playerComponent.Collider = player.Collider;
            playerComponent.WeaponHolder = player.WeaponHolder;
            playerComponent.AimTarget = player.AimTarget;
            playerComponent.CrouchChecker = player.CrouchChecker;
            playerComponent.GroundChecker = player.GroundChecker;
            playerComponent.AnimIK = player.AnimIK;
            
        }
    }
}