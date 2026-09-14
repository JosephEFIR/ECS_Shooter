using Leopotam.Ecs;
using Project.Scripts.Configs;
using Project.Scripts.Core.Player;
using Project.Scripts.Player;
using Project.Scripts.Weapon;
using Project.Scripts.Weapon.Bullet;
using UnityEngine;

namespace Project.Scripts.Core.Enemy.AI.Systems
{
    public class EnemyAttackSystem : IEcsRunSystem
    {
        private readonly EcsWorld _world = null;
        private readonly EcsFilter<EnemyComponent, EnemyUnitComponent, WeaponInventoryComponent> _filter = null;
        
        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var entity = ref _filter.GetEntity(i);
                ref var enemyComp = ref _filter.Get1(i);
                ref var unitComp = ref _filter.Get2(i);
                ref var weaponInventory = ref _filter.Get3(i);
                
                ref var target = ref enemyComp.Target;
                ref var agent = ref unitComp.Agent;
                ref var canSeePlayer = ref enemyComp.CanSeePlayer;
                EnemyUnitConfig config = (EnemyUnitConfig) enemyComp.UnitConfig;
                ref var distanceToTarget = ref enemyComp.DistanceToTarget;
                ref var nextFireTime = ref enemyComp.NextAttackTime;
                
                if (target == null || agent == null || config == null) continue;
                if (!agent.isOnNavMesh) continue;
                
                bool isPlayerTarget = target.GetComponentInParent<PlayerView>() != null;
                if (!isPlayerTarget) continue;
                
                Vector3 aimPoint = GetAimPoint(target);
                
                distanceToTarget = Vector3.Distance(agent.transform.position, aimPoint);
                float attackRange = config.AttackRange;
                
                // 1. ПРЕСЛЕДОВАНИЕ
                if (distanceToTarget > attackRange * 0.8f)
                    agent.SetDestination(target.position);
                else
                    agent.SetDestination(agent.transform.position);
                
                // 2. ПОВОРОТ ТЕЛА (только Y)
                Vector3 flatDir = target.position - agent.transform.position;
                flatDir.y = 0f;
                
                if (flatDir.sqrMagnitude > 0.01f)
                {
                    Quaternion bodyRot = Quaternion.LookRotation(flatDir);
                    agent.transform.rotation = Quaternion.RotateTowards(
                        agent.transform.rotation,
                        bodyRot,
                        config.RotationSpeed * Time.deltaTime);
                }
                
                // 3. AIM TARGET — в центр тела цели
                if (unitComp.AimTarget != null)
                {
                    unitComp.AimTarget.position = aimPoint;
                }
                
                // 4. СТРЕЛЬБА
                if (!canSeePlayer) continue;
                if (distanceToTarget > attackRange) continue;
                if (Time.time < nextFireTime) continue;
                if (weaponInventory.CurrentWeapon.IsNull()) continue;
                
                ref var weapon = ref weaponInventory.CurrentWeapon.Get<WeaponComponent>();
                if (weapon.BulletPool == null) continue;
                
                Shoot(entity, enemyComp, unitComp, ref weapon);
                
                nextFireTime = Time.time + 1f / config.FireRate;
            }
        }
        
        private void Shoot(EcsEntity entity, EnemyComponent enemyComp, EnemyUnitComponent unitComp, ref WeaponComponent weapon)
        {
            BulletView bulletView = weapon.BulletPool.GetObject();
            if (bulletView == null) return;
    
            Transform spawnPoint = weapon.BulletSpawnPoint;
            if (spawnPoint == null) return;
    
            EcsEntity bulletEntity = _world.NewEntity();
            ref var bullet = ref bulletEntity.Get<BulletComponent>();
            bullet.BulletPool = weapon.BulletPool;
            bullet.Owner = enemyComp.Position;
            bulletView.Entity = bulletEntity;
    
            bulletView.transform.position = spawnPoint.position;
            bulletView.transform.rotation = spawnPoint.rotation;
    
            Collider bulletCollider = bulletView.GetComponent<Collider>();
            if (bulletCollider != null)
            {
                Collider[] enemyColliders = enemyComp.View.GetComponentsInChildren<Collider>();
                foreach (var col in enemyColliders)
                    Physics.IgnoreCollision(bulletCollider, col, true);
            }
    
            Rigidbody rb = bulletView.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.linearVelocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                
                Vector3 aimPoint = GetAimPoint(enemyComp.Target);
                Vector3 fireDirection = (aimPoint - spawnPoint.position).normalized;
                rb.linearVelocity = fireDirection * enemyComp.UnitConfig.BulletSpeed;
                
                Debug.DrawLine(spawnPoint.position, aimPoint, Color.red, 1f);
            }
    
            entity.Get<ShootEvent>();
        }
        
        private Vector3 GetAimPoint(Transform target)
        {
            Collider collider = target.GetComponent<Collider>();
            if (collider != null)
                return collider.bounds.center;
            
            Collider[] childColliders = target.GetComponentsInChildren<Collider>();
            foreach (var col in childColliders)
            {
                if (col != null && !col.isTrigger)
                    return col.bounds.center;
            }
            
            return target.position + Vector3.up * 1.5f;
        }
    }
}