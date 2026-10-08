using Leopotam.Ecs;
using Project.Scripts.Animation;
using Project.Scripts.Core.Player;
using Project.Scripts.Tags;
using Project.Scripts.Weapon;

namespace Project.Scripts.Weapon
{
    public class WeaponReloadAnimSystem : IEcsRunSystem
    {
        private readonly EcsFilter<PlayerComponent, WeaponInventoryComponent, AnimationComponent> _filter = null;
        
        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var entity = ref _filter.GetEntity(i);
                ref var weaponInventory = ref _filter.Get2(i);
                ref var animationComp = ref _filter.Get3(i);
                
                ref var weaponEntity = ref weaponInventory.CurrentWeapon;
                if (!weaponEntity.IsAlive()) continue;
                if (!weaponEntity.Has<WeaponComponent>()) continue;
                
                ref var weapon = ref weaponEntity.Get<WeaponComponent>();
                if (weapon.WeaponAnimator == null) continue;

                bool isReloading = entity.Has<WeaponReloadComponent>();
                weapon.WeaponAnimator.SetBool("Reload", isReloading);

                float weaponSpeed = 1f;
                float playerSpeed = 1f;

                if (isReloading && weapon.Config.ReloadTime > 0.01f)
                {
                    weaponSpeed = weapon.Config.WeaponReloadClipLength / weapon.Config.ReloadTime;
                    playerSpeed = weapon.Config.PlayerReloadClipLength / weapon.Config.ReloadTime;
                }

                weapon.WeaponAnimator.speed = weaponSpeed;

                if (animationComp.Animator != null)
                {
                    animationComp.Animator.speed = playerSpeed;
                }
            }
        }
    }
}