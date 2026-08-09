using Leopotam.Ecs;
using Project.Scripts.Core.Player;
using Project.Scripts.Weapon;
using UnityEngine;

namespace Project.Scripts.Animation
{
    public class PlayerWeaponAnimSystem : IEcsRunSystem
    {
        private readonly EcsFilter<PlayerAnimationComponent, WeaponInventoryComponent> _filter = null;
        
        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var animComponent = ref _filter.Get1(i);
                ref var weaponInventory = ref _filter.Get2(i);

                ref var currentWeapon = ref weaponInventory.CurrentWeapon;
                
                if(!currentWeapon.Has<WeaponComponent>()) return;
                
                animComponent.Animator.SetBool(EAnimParameter.IsHasWeapon.ToString(), currentWeapon.Has<WeaponComponent>());
                animComponent.Animator.SetBool(EAnimParameter.Fire.ToString(), currentWeapon.Has<WeaponShootEvent>());
                animComponent.Animator.SetBool(EAnimParameter.Reload.ToString(), currentWeapon.Has<WeaponReloadEvent>());
            }    
        }
    }
}