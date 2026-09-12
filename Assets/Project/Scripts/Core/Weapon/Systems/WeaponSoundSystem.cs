using Leopotam.Ecs;
using Project.Scripts.Configs;
using Project.Scripts.Core.Player;

namespace Project.Scripts.Weapon
{
    public class WeaponSoundSystem : IEcsRunSystem
    {
        private readonly SoundEffectConfig _soundConfig = null;
        private readonly EcsFilter<WeaponInventoryComponent, ShootEvent> _filter = null;
        
        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var weapon = ref _filter.Get1(i).CurrentWeapon.Get<WeaponComponent>();
                ref var audioSource = ref weapon.Audio;
                audioSource.clip = _soundConfig.LaserShoot;
                audioSource.Play();
            }
        }
    }
}