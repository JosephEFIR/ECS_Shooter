using Leopotam.Ecs;
using Project.Scripts.Weapon.Bullet;
using UnityEngine;

namespace Project.Scripts.Weapon
{
    public class WeaponShootSystem : IEcsRunSystem
    {
        private readonly EcsFilter<WeaponComponent, WeaponInputShootEvent> _filter = null;
        private readonly EcsWorld _world = null;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var weapon = ref _filter.Get1(i);
                ref var entity = ref _filter.GetEntity(i);

                ref var totalAmmo = ref weapon.TotalAmmo;
                ref var magazineSize = ref weapon.MagazineSize;
                ref var firerate = ref weapon.FireRate;
                ref var canFire = ref weapon.CanFire;
                ref var isFire = ref weapon.isFire;
                
                if (entity.Has<WeaponReloadComponent>() || totalAmmo.Value <= 0)
                {
                    continue;
                }

                if (magazineSize.Value <= 0)
                {
                    if (!entity.Has<WeaponReloadComponent>())
                    {
                        entity.Get<WeaponReloadComponent>();
                    }
                    isFire = false;
                    continue;
                }

                if (firerate > 0)
                {
                    firerate -= Time.deltaTime * 600f;
                    if (firerate <= 0)
                    {
                        firerate = 0;
                        canFire = true;
                    }
                }

                if (canFire)
                {
                    canFire = false;
                    firerate = weapon.Config.FireRate;
                    Shoot(weapon);
                    entity.Get<ShootEvent>();
                    isFire = true;

                    magazineSize.Value--;
                    totalAmmo.Value--;
                }
            }
        }

        private void Shoot(WeaponComponent weapon)
        {
            BulletView bulletView = weapon.BulletPool.GetObject();
            if (bulletView == null) return;

            EcsEntity bulletEntity = _world.NewEntity();
            ref var bullet = ref bulletEntity.Get<BulletComponent>();
            bullet.BulletPool = weapon.BulletPool;
            bullet.Owner = weapon.Owner;
            bulletView.Entity = bulletEntity;

            bulletView.transform.parent = null;
            bulletView.transform.position = weapon.BulletSpawnPoint.position;
            bulletView.transform.rotation = weapon.BulletSpawnPoint.rotation;
            
            Collider bulletCollider = bulletView.GetComponent<Collider>();
            if (bulletCollider != null && weapon.Owner != null)
            {
                Collider[] ownerColliders = weapon.Owner.GetComponentsInChildren<Collider>();
                foreach (var col in ownerColliders)
                {
                    Physics.IgnoreCollision(bulletCollider, col, true);
                }
            }

            Rigidbody rb = bulletView.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.linearVelocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                rb.AddForce(weapon.BulletSpawnPoint.forward * 50f, ForceMode.Impulse);
            }
        }
    }
}