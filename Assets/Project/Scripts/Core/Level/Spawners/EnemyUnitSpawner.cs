using Leopotam.Ecs;
using Project.Scripts.Core.Enemy.AI;
using UnityEngine;
using Zenject;

namespace Project.Scripts.Level.Spawners
{
    public class EnemyUnitSpawner : MonoBehaviour
    {
        [SerializeField] private PatrolRoomPointsContainer patrolContainer;
        [Inject] private EcsWorld _world;
        
        private void Awake()
        {
            if (_world == null)
            {
                var sceneContext = FindObjectOfType<SceneContext>();
                if (sceneContext is not null)
                {
                    sceneContext.Container.InjectGameObject(gameObject);
                }
                else
                {
                    Debug.LogError("SceneContext not found");
                }
            }
        }

        private void Start()
        {
            EcsEntity Entity = _world.NewEntity();
            Entity.Get<EnemyComponent>();
            Entity.Get<EnemyUnitComponent>();
            ref var patrolPoints = ref Entity.Get<PatrolComponent>().Points;
            patrolPoints = patrolContainer.Points;
            ref var spawnComponent = ref Entity.Get<SpawnComponent>();
            
            spawnComponent.Position = transform;
            spawnComponent.Rotation = transform.rotation;
        }
    }
}