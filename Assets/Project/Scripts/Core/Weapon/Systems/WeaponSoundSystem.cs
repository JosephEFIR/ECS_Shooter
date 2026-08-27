using Leopotam.Ecs;
using Project.Scripts.Configs;

namespace Project.Scripts.Weapon
{
    public class WeaponSoundSystem : IEcsRunSystem
    {
        private readonly SoundEffectConfig _soundConfig = null;
        private readonly EcsFilter<WeaponComponent, ShootEvent> _filter = null;
        
        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var weapon = ref _filter.Get1(i);
                ref var audioSource = ref weapon.Audio;
                audioSource.clip = _soundConfig.LaserShoot;
                audioSource.Play();
            }
        }
    }
}