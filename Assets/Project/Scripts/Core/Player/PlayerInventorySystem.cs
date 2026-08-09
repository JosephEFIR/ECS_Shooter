using Leopotam.Ecs;
using Project.Scripts.Level.Spawners;
using Project.Scripts.Tags;
using Project.Scripts.Weapon;
using UnityEngine;

namespace Project.Scripts.Core.Player
{
    public class PlayerInventorySystem : IEcsRunSystem //REFACTORE THIS
    {
        private readonly EcsWorld _world = null;
        private readonly EcsFilter<PlayerComponent, WeaponInventoryComponent, TakeWeaponEvent> _filter = null;
        
        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var entity = ref _filter.GetEntity(i);
                ref var player = ref _filter.Get1(i);
                ref var weaponInventory = ref _filter.Get2(i);
                ref var config = ref player.Config;
                
                ref var firstWeapon = ref weaponInventory.CurrentWeapon;
                EcsEntity weaponEntity = _world.NewEntity();
                ref var weapon = ref weaponEntity.Get<WeaponComponent>();
                firstWeapon = weaponEntity;
                ref var weaponConfig = ref weapon.Config;
                weaponConfig =  config.Weapon.Config;
                
                ref var spawnComponent = ref weaponEntity.Get<SpawnComponent>();
                spawnComponent.Position = player.WeaponHolder.transform;
                spawnComponent.Rotation = player.WeaponHolder.transform.rotation;
                spawnComponent.Parent = player.WeaponHolder.transform;
                entity.Del<TakeWeaponEvent>();
            }
        }
    }
}