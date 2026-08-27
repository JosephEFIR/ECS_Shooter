using Leopotam.Ecs;
using Project.Scripts.Core.Common;
using Project.Scripts.Core.Health;
using Project.Scripts.Factory.Pool;
using Project.Scripts.Weapon;
using UniRx;
using UnityEngine;

namespace Project.Scripts.Core.Enemy.Turret
{
    public class TurretInitSystem : IEcsRunSystem
    {
        private readonly EcsFilter<TurretComponent, InitComponent> _filter = null;
        
        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var entity = ref _filter.GetEntity(i);
                ref var turretComponent = ref entity.Get<TurretComponent>();
                ref var turretView = ref turretComponent.TurretView;

                turretComponent.Audio = turretView.AudioSource;
                turretComponent.Head = turretView.TurretHad;
                turretComponent.Config = turretView.TurretConfig;
                turretComponent.TurretTransform = turretView.transform;
                turretComponent.InitialRotation = turretView.transform.rotation;
                turretComponent.InitialUp = turretComponent.TurretTransform.up;
                turretComponent.BulletSpawnPoint = turretView.BulletSpawnPoint;
                
                turretComponent.HitBoxObserver = turretView.HitBoxObserver;
                
                turretComponent.TargetsBuffer = new Collider[5];
                //Patrol
                turretComponent.PatrolAngle = 0f;
                turretComponent.PatrolDirection = 1f;
                
                //Pool
                GameObject bulletParent = new GameObject("TurretBulletPool");
                BulletPool pool = turretComponent.BulletPool = new();
                pool.Prefab = turretView.BulletView;
                pool.CreatePool(turretComponent.Config.PoolSize, null, bulletParent.transform);
                
                //Health
                ref var healthComp = ref entity.Get<HealthComponent>();
                healthComp.MaxHealth = turretComponent.Config.Health;
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