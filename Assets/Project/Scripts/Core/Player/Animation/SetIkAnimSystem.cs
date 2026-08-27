using Leopotam.Ecs;
using Project.Scripts.Core.Player;
using Project.Scripts.Tags;
using Project.Scripts.Weapon;

namespace Project.Scripts.Animation
{
    public class SetIkAnimSystem : IEcsRunSystem
    {
        private readonly EcsFilter <PlayerComponent, WeaponInventoryComponent> _filter = null;
        
        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var player = ref _filter.Get1(i);
                ref var weaponInventory = ref _filter.Get2(i);
                ref var weaponEntity = ref weaponInventory.CurrentWeapon;
                ref var weapon = ref weaponInventory.CurrentWeapon.Get<WeaponComponent>();
                ref var playIK = ref player.AnimIK;

                ref var leftHand = ref weapon.LeftHandIKTarget;
                ref var rightHand = ref weapon.RightHandIKTarget;
                ref var rightHint = ref weapon.RightHintIKTarget;
                ref var leftHint = ref weapon.LeftHintIKTarget;
                
                if (weaponEntity.Has<InitializedEvent>())
                {
                    playIK.SetIKTargets(leftHand, rightHand, leftHint, rightHint);
                }
            }
        }
    }
}