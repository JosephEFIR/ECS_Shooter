using Leopotam.Ecs;
using UnityEngine;

namespace Project.Scripts.Weapon
{
    public class WeaponReloadSystem : IEcsRunSystem
    {
        private readonly EcsFilter<WeaponComponent, WeaponReloadComponent> _filter = null;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var entity = ref _filter.GetEntity(i);
                ref var weapon = ref _filter.Get1(i);
                ref var reloadTime = ref weapon.ReloadTime;
                ref var totalAmmo = ref weapon.TotalAmmo;
                ref var magazineSize = ref weapon.MagazineSize;
                ref var isReload = ref weapon.isReload;
                
                if (reloadTime.Value <= 0)
                {
                    reloadTime.Value = weapon.Config.ReloadTime;
                    isReload = true;
                }
                else
                {
                    reloadTime.Value -= Time.deltaTime;

                    if (reloadTime.Value <= 0)
                    {
                        reloadTime.Value = 0;
                        
                        if (totalAmmo.Value < weapon.Config.MagazineSize)
                        {
                            magazineSize.Value = totalAmmo.Value;
                        }
                        else
                        {
                            magazineSize.Value = weapon.Config.MagazineSize;
                        }
                        
                        entity.Del<WeaponReloadComponent>();
                        isReload = false;
                    }
                }
            }
        }
    }
}