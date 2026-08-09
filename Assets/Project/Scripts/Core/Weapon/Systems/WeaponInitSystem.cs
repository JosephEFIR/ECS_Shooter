using Leopotam.Ecs;
using Project.Scripts.Configs.Spawn;
using Project.Scripts.Core.Common;
using Project.Scripts.Factory.Pool;
using Project.Scripts.Weapon.Bullet;
using UnityEngine;

namespace Project.Scripts.Weapon
{
    sealed class WeaponInitSystem : IEcsRunSystem
    {
        private readonly EcsFilter<WeaponComponent, InitComponent> _filter = null;
            
        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var entity = ref _filter.GetEntity(i);
                ref var weaponComponent = ref _filter.Get1(i);
                ref var config = ref weaponComponent.Config;

                weaponComponent.TotalAmmo = config.TotalAmmo;
                weaponComponent.FireRate = config.FireRate;
                weaponComponent.MagazineSize = config.MagazineSize;
                weaponComponent.ReloadTime = config.ReloadTime;
                weaponComponent.active = false;
                
                BulletView bulletView = config.BulletView;
                weaponComponent.BulletPool = CreateBullets(config.MagazineSize, bulletView, weaponComponent.BulletSpawnPoint);
                
                entity.Del<InitComponent>();
                entity.Get<InitializedEvent>();
                Debug.LogWarning("Weapon initialized");
            }
        }

        private BulletPool CreateBullets(int count, BulletView bulletView, Transform spawnPos)
        {
            BulletPool bulletPool = new BulletPool();
            bulletPool.Prefab = bulletView;
            bulletPool.CreatePool(count, spawnPos, spawnPos.parent);
            return bulletPool;
        }
    }
}