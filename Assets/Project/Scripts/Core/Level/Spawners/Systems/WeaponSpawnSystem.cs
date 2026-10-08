using Leopotam.Ecs;
using Project.Scripts.Configs.Spawn;
using Project.Scripts.Core.Common;
using Project.Scripts.Factory;
using Project.Scripts.Level.Spawners;
using Project.Scripts.Player;
using Project.Scripts.Weapon;
using UnityEngine;

namespace Project.Scripts.Core.Level.Spawners.Systems
{
    sealed class WeaponSpawnSystem : IEcsRunSystem
    {
        private readonly WeaponFactory _factory = null;
        private readonly SpawnConfig _spawnConfig = null;
        private readonly EcsFilter<WeaponComponent, SpawnComponent> _filter = null;
        
        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var entity = ref _filter.GetEntity(i);
                ref var spawnComponent = ref _filter.Get2(i);

                SpawnWeapon(entity, spawnComponent.Position, spawnComponent.Rotation, spawnComponent.Parent);
                
                entity.Get<InitComponent>();
                entity.Del<SpawnComponent>();
            }
        }

        private void SpawnWeapon(EcsEntity entity, Transform weaponTransform, Quaternion rotation, Transform parent = null)
        {
            WeaponView weaponView = _factory.Create(_spawnConfig.WeaponPrefab, weaponTransform.position, rotation, parent);

            if (parent != null)
            {
                weaponView.transform.localPosition = Vector3.zero;
                weaponView.transform.localRotation = Quaternion.identity;

                PlayerView playerView = parent.GetComponentInParent<PlayerView>();
                if (playerView != null)
                {
                    WeaponIkTargets targets = weaponView.GetComponentInChildren<WeaponIkTargets>();
                    if (targets != null)
                    {
                        playerView.AnimIK.SetIKTargets(
                            targets.RightHand,
                            targets.RightElbow,
                            targets.LeftHand,
                            targets.LeftElbow);
                    }

                    WeaponMagazine weaponMagazine = weaponView.GetComponentInChildren<WeaponMagazine>();
                    if (weaponMagazine != null && playerView.LeftHand != null)
                    {
                        weaponMagazine.SetPlayerLeftHand(playerView.LeftHand);
                    }
                }
            }

            WeaponComponentInit(entity, weaponView);
        }

        private void WeaponComponentInit(EcsEntity weaponEntity, WeaponView weaponView)
        {
            ref var weaponComponent = ref weaponEntity.Get<WeaponComponent>();
            weaponComponent.Config = weaponView.Config;
            weaponComponent.BulletSpawnPoint = weaponView.BulletSpawnPoint;
            weaponComponent.Audio = weaponView.Audio;
            weaponComponent.WeaponAnimator = weaponView.WeaponAnimator;
            weaponComponent.LeftHandIKTarget = weaponView.LeftHandIKTarget;
            weaponComponent.RightHandIKTarget = weaponView.RightHandIKTarget;
            weaponComponent.RightHintIKTarget = weaponView.RightHintIKTarget;
            weaponComponent.LeftHintIKTarget = weaponView.LeftHintIKTarget;
        }
    }
}