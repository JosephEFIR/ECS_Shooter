using Leopotam.Ecs;
using Project.Scripts.Tags;
using Project.Scripts.Weapon;

namespace Project.Scripts.Core.Health.Systems
{
    public class PlayerHealthInitSystem : IEcsRunSystem
    {
        private readonly EcsFilter<PlayerComponent, InitializedEvent> _filter = null;
        
        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var entity = ref _filter.GetEntity(i);
                ref var playerComponent = ref _filter.Get1(i);

                if (!entity.Has<HealthComponent>())
                {
                    return;
                }
                ref var healthComponent = ref entity.Get<HealthComponent>();
                healthComponent.MaxHealth = playerComponent.Config.Health;
                healthComponent.CurrentHealth = new();
                healthComponent.CurrentHealth.Value = healthComponent.MaxHealth;
            }
        }
    }
}