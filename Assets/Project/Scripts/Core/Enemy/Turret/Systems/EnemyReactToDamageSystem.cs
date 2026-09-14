using Leopotam.Ecs;
using Project.Scripts.Core.Common;
using Project.Scripts.Core.Enemy.AI;
using Project.Scripts.Player;

namespace Project.Scripts.Core.Enemy.Turret
{
    public class EnemyReactToDamageSystem : IEcsRunSystem
    {
        private readonly EcsFilter<EnemyComponent,TakeDamageEvent> _filter = null;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var entity = ref _filter.GetEntity(i);
                ref var enemyComp = ref _filter.Get1(i);
                ref var takeDamageEvent = ref _filter.Get2(i);

                if (takeDamageEvent.Attacker != null && takeDamageEvent.Attacker.TryGetComponent(typeof(PlayerView), out var player))
                {
                    enemyComp.Target = takeDamageEvent.Attacker;
                    enemyComp.Aggroed = true;
                }

                entity.Del<TakeDamageEvent>();
            }
        }
    }
}