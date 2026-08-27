using Leopotam.Ecs;
using Project.Scripts.Weapon;
using UniRx;

namespace Project.Scripts.Core.Health.Systems
{
    public class DeathSendEventSystem : IEcsRunSystem, IEcsDestroySystem
    {
        private readonly EcsFilter<HealthComponent, InitializedEvent> _filter = null;
		
        private readonly CompositeDisposable _disposable = new();
        
        public void Run()
        {
            foreach (var i in _filter)
            {
                var entity = _filter.GetEntity(i);
                ref var healthComponent = ref _filter.Get1(i);
                var currentHealth = healthComponent.CurrentHealth;
                
                currentHealth.Subscribe(v =>
                {
                    if (v <= 0)
                    {
                        entity.Get<DeathEvent>();
                    }
                }).AddTo(_disposable);
            }
        }
        
        public void Destroy()
        {
            _disposable?.Clear();
            _disposable?.Dispose();
        }
    }
}