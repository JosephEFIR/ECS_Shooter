using Project.Scripts.UI.Health;
using Project.Scripts.UI.Weapon.Enemy;
using UniRx;

namespace Project.Scripts.Core.Health
{
    internal struct HealthComponent
    {
        public ReactiveProperty<float> CurrentHealth;
        public float MaxHealth;
        public HealthUIView HealthUIView;
    }
}