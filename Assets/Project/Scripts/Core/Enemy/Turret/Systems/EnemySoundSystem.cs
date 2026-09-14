using Leopotam.Ecs;
using Project.Scripts.Configs;
using Project.Scripts.Core.Enemy.AI;
using Project.Scripts.Weapon;

namespace Project.Scripts.Core.Enemy.Turret
{
    public class EnemySoundSystem : IEcsRunSystem
    {
        private readonly SoundEffectConfig _soundConfig = null;
        private readonly EcsFilter<EnemyComponent, ShootEvent> _filter = null;
        
        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var enemyComp = ref _filter.Get1(i);
                ref var audioSource = ref enemyComp.Audio;
                audioSource.clip = _soundConfig.LaserShoot;
                audioSource.Play();
            }
        }
    }
}