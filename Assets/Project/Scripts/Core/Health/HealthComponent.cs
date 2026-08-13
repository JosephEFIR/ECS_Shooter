using UniRx;

namespace Project.Scripts.Core.Health
{
    internal struct HealthComponent
    {
        public ReactiveProperty<float> CurrentHealth;
        public float MaxHealth;
    }
}