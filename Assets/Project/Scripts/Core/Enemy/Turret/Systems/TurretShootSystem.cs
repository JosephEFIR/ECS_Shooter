using Leopotam.Ecs;
using Project.Scripts.Core.Enemy.AI;
using Project.Scripts.Weapon;
using Project.Scripts.Weapon.Bullet;
using UnityEngine;

namespace Project.Scripts.Core.Enemy.Turret
{
    public class TurretShootSystem : IEcsRunSystem
    {
        private readonly EcsWorld _world = null;
        private readonly EcsFilter<EnemyComponent,TurretComponent> _filter = null;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var entity = ref _filter.GetEntity(i);
                ref var enemyComp = ref _filter.Get1(i);
                ref var turretComp = ref _filter.Get2(i);
                
                ref var config = ref enemyComp.UnitConfig;
                ref var target = ref enemyComp.Target;
                ref var bulletPool = ref enemyComp.BulletPool;
                ref var canSeePlayer = ref enemyComp.CanSeePlayer;
                ref var nextFireTime = ref enemyComp.NextAttackTime;
                ref var bulletSpawnPoint = ref turretComp.BulletSpawnPoint;

                if (!canSeePlayer || target == null) continue;
                if (bulletPool == null || bulletSpawnPoint == null) continue;
                if (Time.time < nextFireTime) continue;

                BulletView bulletView = bulletPool.GetObject();
                if (bulletView == null) continue;

                EcsEntity bulletEntity = _world.NewEntity();
                ref var bullet = ref bulletEntity.Get<BulletComponent>();
                bullet.BulletPool = bulletPool;
                bullet.Owner = enemyComp.Position;
                bulletView.Entity = bulletEntity;

                bulletView.transform.position = bulletSpawnPoint.position;
                bulletView.transform.rotation = bulletSpawnPoint.rotation;

                Collider bulletCollider = bulletView.GetComponent<Collider>();
                if (bulletCollider != null)
                {
                    Collider[] turretColliders = enemyComp.View.GetComponentsInChildren<Collider>();
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