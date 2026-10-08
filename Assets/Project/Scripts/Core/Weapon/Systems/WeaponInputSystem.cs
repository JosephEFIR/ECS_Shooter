using Leopotam.Ecs;
using Project.Scripts.Core.Player;
using Project.Scripts.Tags;
using UnityEngine;

namespace Project.Scripts.Weapon
{
    public class WeaponInputSystem : IEcsRunSystem
    {
        private readonly EcsFilter<PlayerComponent,WeaponInventoryComponent> _filter = null;

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
                entity.Get<WeaponInputShootEvent>();
            }
            else if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                entity.Del<WeaponInputShootEvent>();
            }
        }

        private void Reload(EcsEntity entity)
        {
            ref var weaponInventory = ref entity.Get<WeaponInventoryComponent>();
            if (!weaponInventory.CurrentWeapon.IsAlive()) return;
            if (!weaponInventory.CurrentWeapon.Has<WeaponComponent>()) return;

            ref var weapon = ref weaponInventory.CurrentWeapon.Get<WeaponComponent>();
            ref var totalAmmo = ref weapon.TotalAmmo;
            ref var magazineSize = ref weapon.MagazineSize;

            if (totalAmmo.Value == 0) return;
            if (magazineSize.Value >= weapon.Config.MagazineSize) return;

            if (Input.GetKey(KeyCode.R) && !entity.Has<WeaponReloadComponent>())
            {
                entity.Get<WeaponReloadComponent>();
            }
        }
    }
}