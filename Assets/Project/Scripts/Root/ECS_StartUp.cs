using Leopotam.Ecs;
using Project.Scripts.Animation;
using Project.Scripts.Configs.Spawn;
using Project.Scripts.Core.Level.Spawners.Systems;
using Project.Scripts.Core.Player;
using Project.Scripts.Factory;
using Project.Scripts.Move;
using Project.Scripts.Other;
using Project.Scripts.UI.Weapon;
using Project.Scripts.Weapon;
using Project.Scripts.Weapon.Bullet;
using UnityEngine;
using Voody.UniLeo;
using Zenject;

namespace Project.Scripts.Common
{
    sealed class ECS_StartUp : MonoBehaviour
    {
        [Inject] private EcsWorld _world;
        [Inject] private PlayerFactory _playerFactory;
        [Inject] private WeaponFactory _weaponFactory;
        [Inject] private UiView _uiView;
        [Inject] private SpawnConfig _spawnConfig;
        
        private EcsSystems _systems;

        private void Start()
        {
            _systems = new EcsSystems(_world);
            _systems.ConvertScene();
            AddInjections();
            AddOneFrames();
            AddSystems();
            
            _systems.Init();
            Debug.Log("Systems initialized");
        }

        private void Update()
        {
            _systems?.Run();
        }

        private void AddOneFrames()
        {
            _systems
                .OneFrame<JumpEvent>()
                .OneFrame<CameraSwitchEvent>()
                .OneFrame<WeaponShootEvent>()
                .OneFrame<BulletTriggerEvent>()
                .OneFrame<InitializedEvent>()
                ;
        }

        private void AddInjections()
        {
            _systems
                .Inject(_playerFactory)
                .Inject(_weaponFactory)
                .Inject(_spawnConfig)
                .Inject(_uiView)
                ;
        }

        private void AddSystems() 
        {
            PlayerSystems();
            WeaponSystems();
            UISystems();
        }

        private void PlayerSystems() // мб системы в другое место?
        {
            _systems
                .Add(new PlayerSpawnSystem())
                
                .Add(new PlayerInitSystem())
                .Add(new PlayerInventorySystem())
                .Add(new PlayerJumpSendEventSystem())
                //.Add(new CameraSwitcherSendEventSystem()) //TODO на доработке
                .Add(new PlayerGroundCheckSystem())
                .Add(new PlayerInputSystem())
                .Add(new PlayerMovementSystem())
                .Add(new PlayerCrouchSystem())
                .Add(new PlayerMouseInputSystem())
                .Add(new PlayerMouseLookSystem())
                .Add(new PlayerJumpSystem())
                .Add(new PlayerAnimationSystem())
                .Add(new CameraSwitcherSystem())
                .Add(new CursorLockedSystem())
                .Add(new MousePositionSystem())
                ;
        }

        private void WeaponSystems()
        {
            _systems
                .Add(new WeaponSpawnSystem())
                .Add(new WeaponInitSystem())
                .Add(new WeaponInputSystem())
                .Add(new WeaponShootSystem())
                .Add(new WeaponReloadSystem())
                .Add(new PlayerWeaponAnimSystem())
                .Add(new SetIkAnimSystem())
                ;

        }

        private void UISystems()
        {
            _systems
                .Add(new TotalAmmoUIInitSystem())
                ;
        }

        private void OnDestroy()
        {
            if(_systems is null) return;
            
            _systems.Destroy();
            _systems = null;
            
            _world.Destroy();
            _world = null;
        }
    }
}