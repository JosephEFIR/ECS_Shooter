using Leopotam.Ecs;
using Project.Scripts.Core.Health;
using Project.Scripts.Weapon;
using UniRx;

namespace Project.Scripts.UI.Health
{
    public class HealthUIInitSystem : IEcsRunSystem, IEcsDestroySystem
    {
		private readonly EcsFilter<HealthComponent, InitializedEvent> _filter = null;
		
		private readonly CompositeDisposable _disposable = new();

        public void Run()
		{
			foreach (var i in _filter)
			{
				ref var healthComponent = ref _filter.Get1(i);
				var currentHealth = healthComponent.CurrentHealth;
				var uiView = healthComponent.HealthUIView;
				
				uiView.TinyHealthSystem.SetHealth(healthComponent.MaxHealth);
				currentHealth.Pairwise().Subscribe(pair =>
				{
					var healthSystem = uiView.TinyHealthSystem;
					float previous = pair.Previous;
					float current = pair.Current;

					if (current < previous)
					{
						healthSystem.TakeDamage(previous - current);
					}
					else if (current > previous)
					{
						healthSystem.HealDamage(current - previous);
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