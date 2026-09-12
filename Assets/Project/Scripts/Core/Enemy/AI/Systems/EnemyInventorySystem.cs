using Leopotam.Ecs;
using Project.Scripts.Configs;
using Project.Scripts.Core.Player;
using Project.Scripts.Level.Spawners;
using Project.Scripts.Weapon;

namespace Project.Scripts.Core.Enemy.AI.Systems
{
    public class EnemyInventorySystem : IEcsRunSystem //TODO DRY!!!!!!
    {
        private readonly EcsWorld _world = null;
        private readonly EcsFilter<EnemyUnitComponent,EnemyComponent, WeaponInventoryComponent, TakeWeaponEvent> _filter = null;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var entity = ref _filter.GetEntity(i);
                ref var enemyUnitComp = ref _filter.Get1(i);
                ref var enemy = ref _filter.Get2(i);
                ref var weaponInventory = ref _filter.Get3(i);
                EnemyUnitConfig config = (EnemyUnitConfig) enemy.UnitConfig;

                ref var firstWeapon = ref weaponInventory.CurrentWeapon;
                EcsEntity weaponEntity = _world.NewEntity();
                ref var weapon = ref weaponEntity.Get<WeaponComponent>();

                weapon.Owner = enemy.Position;

                firstWeapon = weaponEntity;
                ref var weaponConfig = ref weapon.Config;
                weaponConfig = config.Weapon.Config;

                ref var spawnComponent = ref weaponEntity.Get<SpawnComponent>();
                spawnComponent.Position = enemyUnitComp.WeaponHolder.transform;
                spawnComponent.Rotation = enemyUnitComp.WeaponHolder.transform.rotation;
                spawnComponent.Parent = enemyUnitComp.WeaponHolder.transform;
                entity.Del<TakeWeaponEvent>();
            }
        }
    }
}