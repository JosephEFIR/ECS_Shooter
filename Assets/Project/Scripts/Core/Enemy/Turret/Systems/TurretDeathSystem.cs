using Leopotam.Ecs;
using Project.Scripts.Core.Health;
using Project.Scripts.Factory.Pool;
using UnityEngine;

namespace Project.Scripts.Core.Enemy.Turret
{
    public class TurretDeathSystem : IEcsRunSystem
    {
        private readonly EcsFilter<TurretComponent, DeathEvent> _filter = null;
        
        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var entity = ref _filter.GetEntity(i);
                ref var turretComponent = ref _filter.Get1(i);
                
                GameObject.Destroy(turretComponent.TurretView.gameObject);
                entity.Destroy();
            }
        }
    }
}