using Leopotam.Ecs;
using Project.Scripts.Core.Enemy.AI;
using Project.Scripts.Core.Health;
using UnityEngine;

namespace Project.Scripts.Core.Enemy.Turret
{
    public class TurretDeathSystem : IEcsRunSystem
    {
        private readonly EcsFilter<EnemyComponent,TurretComponent, DeathEvent> _filter = null;
        
        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var entity = ref _filter.GetEntity(i);
                ref var enemyComp = ref _filter.Get1(i);
                
                GameObject.Destroy(enemyComp.View.gameObject);
                entity.Destroy();
            }
        }
    }
}