using Leopotam.Ecs;
using UnityEngine;

namespace Project.Scripts.Weapon
{
    public class WeaponReloadSystem : IEcsRunSystem
    {
        private readonly EcsFilter<WeaponComponent, WeaponReloadEvent> _filter = null;
        
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

                if (reloadTime <= 0)
                {
                    reloadTime = weapon.Config.ReloadTime;
                    entity.Del<WeaponReloadEvent>();
                    isReload = false;
                }
                else
                {
                    isReload = true;
                    reloadTime -= Time.deltaTime;
                    if (reloadTime <= 0)
                    {
                        reloadTime = 0;
                        
                        if (totalAmmo < weapon.Config.MagazineSize)
                        {
                            magazineSize = totalAmmo;
                        }                                                                                                     
                        else magazineSize = weapon.Config.MagazineSize;
                    }
                }
            }
        }
    }
}