using Leopotam.Ecs;
using Project.Scripts.Animation;
using Project.Scripts.Configs;
using Project.Scripts.Core.Common;
using Project.Scripts.Core.Health;
using Project.Scripts.Core.Player;
using Project.Scripts.Weapon;
using UnityEngine;

namespace Project.Scripts.Core.Enemy.AI.Systems
{
    public class EnemyUnitInitSystem : IEcsRunSystem
    {
        private readonly EcsFilter<EnemyUnitComponent, EnemyComponent, InitComponent> _filter = null;
        
        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var entity = ref _filter.GetEntity(i);
                ref var enemyUnitComp = ref _filter.Get1(i);
                ref var enemyComp = ref _filter.Get2(i);
                
                EnemyUnitView view = (EnemyUnitView)enemyComp.View;
                EnemyUnitConfig config = (EnemyUnitConfig)view.Config;
                
                enemyComp.UnitConfig = config;
                enemyComp.Position = view.transform;
                enemyUnitComp.AnimIK = view.Animik;
                enemyComp.TargetsBuffer = new Collider[5];
                enemyComp.Audio = view.AudioSource;
                
                enemyUnitComp.WeaponHolder = view.WeaponHolder;
                enemyUnitComp.Agent = view.Agent;
                enemyUnitComp.AimTarget = view.AimTarget;
                
                //Animation
                ref var animcomponent = ref entity.Get<AnimationComponent>();
                animcomponent.Animator = view.Animator;
                animcomponent.AnimIK = view.Animik;
                
                //Weapon
                entity.Get<WeaponInventoryComponent>();
                entity.Get<TakeWeaponEvent>();
                
                //Health
                ref var healthComp = ref entity.Get<HealthComponent>();
                healthComp.MaxHealth = enemyComp.UnitConfig.Health;
                healthComp.CurrentHealth = new();   
                healthComp.CurrentHealth.Value = healthComp.MaxHealth;
                
                //HitBox
                ref var hitboxComp = ref entity.Get<HitBoxComponent>();
                hitboxComp.HitBoxObserver = view.HitBoxObserver;
                hitboxComp.HitBoxObserver.Entity = entity;
                
                
                
                entity.Get<InitializedEvent>();
                entity.Del<InitComponent>();
            }
        }
    }
}