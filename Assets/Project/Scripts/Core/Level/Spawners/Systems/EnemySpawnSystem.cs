using Leopotam.Ecs;
using Project.Scripts.Configs.Spawn;
using Project.Scripts.Core.Common;
using Project.Scripts.Core.Enemy.AI;
using Project.Scripts.Factory;
using Project.Scripts.Level.Spawners;
using UnityEngine;

namespace Project.Scripts.Core.Level.Spawners.Systems
{
    public class EnemySpawnSystem : IEcsRunSystem
    {
        private readonly EnemyFactory _factory = null; 
        private readonly SpawnConfig _spawnConfig = null;
        private readonly EcsFilter<EnemyComponent, SpawnComponent> _filter = null;
        
        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var entity = ref _filter.GetEntity(i);
                ref var spawnComponent = ref _filter.Get2(i);

                Spawn(entity,spawnComponent.Position, spawnComponent.Rotation, spawnComponent.Parent);
                
                entity.Get<InitComponent>();
                entity.Del<SpawnComponent>();
            }
        }
        
        private void Spawn(EcsEntity entity,Transform enemyTransform, Quaternion rotation, Transform parent = null)
        {
            BaseEnemyView baseEnemyView = _factory.Create(_spawnConfig.EnemyPrefab, enemyTransform.position, rotation, parent);
            EnemyComponentInit(entity, baseEnemyView);
        }

        private void EnemyComponentInit(EcsEntity entity, BaseEnemyView baseEnemyView)
        {
            ref var enemyComp = ref entity.Get<EnemyComponent>();
            enemyComp.View = baseEnemyView;
        }
    }
}