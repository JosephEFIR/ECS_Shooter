using Leopotam.Ecs;
using UnityEngine;

namespace Project.Scripts.Weapon
{
    public class WeaponInputSystem : IEcsRunSystem
    {
        private readonly EcsFilter<WeaponComponent> _filter = null;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var entity = ref _filter.GetEntity(i);
                Shoot(entity);
                Reload(entity);
            }
        }

        private void Shoot(EcsEntity entity)
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                entity.Get<WeaponShootEvent>();
            }
            else if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                entity.Del<WeaponShootEvent>();
            }
        }

        private void Reload(EcsEntity entity)
        {
            ref var weapon = ref entity.Get<WeaponComponent>();
            ref var totalAmmo = ref weapon.TotalAmmo;
            
            if(totalAmmo.Value == 0) return;
            
            if (Input.GetKey(KeyCode.R) && !entity.Has<WeaponReloadComponent>())
            {
                entity.Get<WeaponReloadComponent>();
            }
        }
    }
}