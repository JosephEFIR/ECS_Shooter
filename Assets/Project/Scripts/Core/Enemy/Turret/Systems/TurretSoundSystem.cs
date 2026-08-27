using Leopotam.Ecs;
using Project.Scripts.Configs;
using Project.Scripts.Weapon;

namespace Project.Scripts.Core.Enemy.Turret
{
    public class TurretSoundSystem : IEcsRunSystem
    {
        private readonly SoundEffectConfig _soundConfig = null;
        private readonly EcsFilter<TurretComponent, ShootEvent> _filter = null;
        
        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var turret = ref _filter.Get1(i);
                ref var audioSource = ref turret.Audio;
                audioSource.clip = _soundConfig.LaserShoot;
                audioSource.Play();
            }
        }
    }
}