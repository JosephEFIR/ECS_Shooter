using Leopotam.Ecs;
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

                weaponComponent.TotalAmmo = new();
                weaponComponent.MagazineSize = new();
                weaponComponent.ReloadTime = new();
                
                weaponComponent.TotalAmmo.Value = config.TotalAmmo;
                weaponComponent.MagazineSize.Value = config.MagazineSize;
                weaponComponent.ReloadTime.Value = config.ReloadTime;
                weaponComponent.FireRate = config.FireRate;
                weaponComponent.active = false;
                
                BulletView bulletView = config.BulletView;
                weaponComponent.BulletPool = CreateBullets(config.MagazineSize, bulletView);
                
                entity.Del<InitComponent>();
                entity.Get<InitializedEvent>();
                Debug.LogWarning("Weapon initialized");
            }
        }

        private BulletPool CreateBullets(int count, BulletView bulletView)
        {
            GameObject bulletParent = new GameObject("BulletPool");
            BulletPool bulletPool = new BulletPool();
            bulletPool.Prefab = bulletView;
            bulletPool.CreatePool(count, null, bulletParent.transform);
            return bulletPool;
        }
    }
}