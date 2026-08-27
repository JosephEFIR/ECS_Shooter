using Leopotam.Ecs;
using Project.Scripts.Core.Common;
using Project.Scripts.Player;

namespace Project.Scripts.Core.Enemy.Turret
{
    public class TurretReactToDamageSystem : IEcsRunSystem
    {
        private readonly EcsFilter<TurretComponent, TakeDamageEvent> _filter = null;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var entity = ref _filter.GetEntity(i);
                ref var turretComp = ref _filter.Get1(i);
                ref var takeDamageEvent = ref _filter.Get2(i);

                if (takeDamageEvent.Attacker != null && takeDamageEvent.Attacker.TryGetComponent(typeof(PlayerView), out var player))
                {
                    turretComp.Target = takeDamageEvent.Attacker;
                    turretComp.Aggroed = true;
                }

                entity.Del<TakeDamageEvent>();
            }
        }
    }
}