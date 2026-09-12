using Leopotam.Ecs;
using Project.Scripts.Configs;
using Project.Scripts.Core.Common;
using Project.Scripts.Core.Enemy.AI;
using Project.Scripts.Core.Health;
using Project.Scripts.Factory.Pool;
using Project.Scripts.Weapon;
using UnityEngine;

namespace Project.Scripts.Core.Enemy.Turret
{
    public class TurretInitSystem : IEcsRunSystem
    {
        private readonly EcsFilter<EnemyComponent, TurretComponent, InitComponent> _filter = null;
        
        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var entity = ref _filter.GetEntity(i);
                ref var turretComponent = ref _filter.Get2(i);
                ref var enemyComp = ref _filter.Get1(i);
                TurretView turretView = (TurretView)enemyComp.View;
                TurretUnitConfig config = (TurretUnitConfig) turretView.Config;

                enemyComp.UnitConfig = config;
                enemyComp.Audio = turretView.AudioSource;
                enemyComp.Position = turretView.transform;
                
                turretComponent.Head = turretView.TurretHad;
                turretComponent.InitialRotation = turretView.transform.rotation;
                turretComponent.InitialUp = turretView.transform.up;
                turretComponent.BulletSpawnPoint = turretView.BulletSpawnPoint;
                turretComponent.HitBoxObserver = turretView.HitBoxObserver;
                
                enemyComp.TargetsBuffer = new Collider[5];
                //Patrol
                ref var patrolComp = ref entity.Get<PatrolComponent>();
                patrolComp.PatrolRange = config.PatrolRange;
                patrolComp.PatrolSpeed = config.PatrolSpeed;
                patrolComp.PatrolAngle = 0f;
                patrolComp.PatrolDirection = 1f;
                
                //Pool
                GameObject bulletParent = new GameObject("TurretBulletPool");
                BulletPool pool = enemyComp.BulletPool = new();
                pool.Prefab = turretView.BulletView;
                pool.CreatePool(enemyComp.UnitConfig.PoolSize, null, bulletParent.transform);
                
                //Health
                ref var healthComp = ref entity.Get<HealthComponent>();
                healthComp.MaxHealth = enemyComp.UnitConfig.Health;
                healthComp.CurrentHealth = new();   
                healthComp.CurrentHealth.Value = healthComp.MaxHealth;
                healthComp.HealthUIView = turretView.HealthUIView;
                
                //Hitbox
                ref var hitBox = ref entity.Get<HitBoxComponent>();
                hitBox.HitBoxObserver = turretComponent.HitBoxObserver;
                hitBox.HitBoxObserver.Entity = entity;
                
                entity.Del<InitComponent>();
                Debug.Log("Turret initialized");
                entity.Get<InitializedEvent>();
            }
        }
    }
}