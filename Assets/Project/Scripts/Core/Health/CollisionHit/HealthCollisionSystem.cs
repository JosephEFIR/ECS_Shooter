using Leopotam.Ecs;
using Project.Scripts.Core.Health;
using Project.Scripts.Weapon.Bullet;
using UnityEngine;

namespace Project.Scripts.Core.Common
{
    public class HealthCollisionSystem : IEcsRunSystem
    {
        private readonly EcsFilter<HitBoxComponent, HealthComponent, CollisionEnterEvent> _filter = null;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var entity = ref _filter.GetEntity(i);
                ref var hitboxComp = ref _filter.Get1(i);
                ref var healthComp = ref _filter.Get2(i);
                ref var colliderEntered = ref hitboxComp.ColliderEntered;

                if (colliderEntered is null) continue;

                if (colliderEntered.gameObject.TryGetComponent(out BulletView bullet))
                {
                    if (healthComp.CurrentHealth.Value <= 0) continue;
                    Transform attacker = null;
                    if (bullet.Entity.IsAlive() && bullet.Entity.Has<BulletComponent>())
                    {
                        ref var bulletComp = ref bullet.Entity.Get<BulletComponent>();
                        attacker = bulletComp.Owner;
                    }
                    if (attacker != hitboxComp.HitBoxObserver.transform)
                    {
                        healthComp.CurrentHealth.Value -= Random.Range(3, 5);
                        ref var takeDamageEvent = ref entity.Get<TakeDamageEvent>();
                        takeDamageEvent.Attacker = attacker;
                    }
                    
                }

                entity.Del<CollisionEnterEvent>();
            }
        }
    }
}