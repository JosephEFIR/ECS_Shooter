using Leopotam.Ecs;
using Project.Scripts.Weapon;
using Project.Scripts.Weapon.Bullet;
using UnityEngine;

namespace Project.Scripts.Core.Enemy.Turret
{
    public class TurretShootSystem : IEcsRunSystem
    {
        private readonly EcsWorld _world = null;
        private readonly EcsFilter<TurretComponent> _filter = null;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var entity = ref _filter.GetEntity(i);
                ref var turretComp = ref _filter.Get1(i);
                ref var config = ref turretComp.Config;
                ref var target = ref turretComp.Target;
                ref var bulletPool = ref turretComp.BulletPool;
                ref var canSeePlayer = ref turretComp.CanSeePlayer;
                ref var nextFireTime = ref turretComp.NextFireTime;
                ref var bulletSpawnPoint = ref turretComp.BulletSpawnPoint;

                if (!canSeePlayer || target == null) continue;
                if (bulletPool == null || bulletSpawnPoint == null) continue;
                if (Time.time < nextFireTime) continue;

                BulletView bulletView = bulletPool.GetObject();
                if (bulletView == null) continue;

                EcsEntity bulletEntity = _world.NewEntity();
                ref var bullet = ref bulletEntity.Get<BulletComponent>();
                bullet.BulletPool = bulletPool;
                bullet.Owner = turretComp.TurretTransform;
                bulletView.Entity = bulletEntity;

                bulletView.transform.position = bulletSpawnPoint.position;
                bulletView.transform.rotation = bulletSpawnPoint.rotation;

                Collider bulletCollider = bulletView.GetComponent<Collider>();
                if (bulletCollider != null)
                {
                    Collider[] turretColliders = turretComp.TurretView.GetComponentsInChildren<Collider>();
                    foreach (var turretCol in turretColliders)
                    {
                        Physics.IgnoreCollision(bulletCollider, turretCol, true);
                    }
                }

                Rigidbody rb = bulletView.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.linearVelocity = Vector3.zero;
                    rb.angularVelocity = Vector3.zero;
                    
                    Vector3 aimPoint = GetAimPoint(target);
                    Vector3 fireDirection = (aimPoint - bulletSpawnPoint.position).normalized;
                    rb.linearVelocity = fireDirection * config.BulletSpeed;
                    
                    Debug.DrawLine(bulletSpawnPoint.position, aimPoint, Color.green, 1f);
                }

                entity.Get<ShootEvent>();
                nextFireTime = Time.time + 1f / config.FireRate;
            }
        }
        
        private Vector3 GetAimPoint(Transform target)
        {
            Collider collider = target.GetComponent<Collider>();
            if (collider != null)
            {
                return collider.bounds.center;
            }
            
            Collider[] childColliders = target.GetComponentsInChildren<Collider>();
            if (childColliders.Length > 0)
            {
                foreach (var col in childColliders)
                {
                    if (col != null && !col.isTrigger)
                    {
                        return col.bounds.center;
                    }
                }
            }
            
            return target.position;
        }
    }
}