using Leopotam.Ecs;
using Project.Scripts.Core.Player;
using Project.Scripts.Weapon;

namespace Project.Scripts.Animation
{
    public class SetIkAnimSystem : IEcsRunSystem
    {
        private readonly EcsFilter<AnimationComponent, WeaponInventoryComponent> _filter = null;
        
        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var entity = ref _filter.GetEntity(i);
                ref var animationComp = ref _filter.Get1(i);
                ref var weaponInventory = ref _filter.Get2(i);
                ref var animIK = ref animationComp.AnimIK;

                if (animIK == null) continue;

                ref var weaponEntity = ref weaponInventory.CurrentWeapon;
                if (!weaponEntity.IsAlive()) continue;
                if (!weaponEntity.Has<WeaponComponent>()) continue;

                ref var weapon = ref weaponEntity.Get<WeaponComponent>();

                animIK.SetIKTargets(
                    weapon.LeftHandIKTarget,
                    weapon.RightHandIKTarget,
                    weapon.LeftHintIKTarget,
                    weapon.RightHintIKTarget);

                bool isReloading = entity.Has<WeaponReloadComponent>();

                animIK.SetLeftHandActive(!isReloading);
            }
        }
    }
}