using Leopotam.Ecs;

namespace Project.Scripts.Core.Player
{
    internal struct WeaponInventoryComponent
    {
        public EcsEntity CurrentWeapon;
        public EcsEntity SecondWeapon;
    }
}