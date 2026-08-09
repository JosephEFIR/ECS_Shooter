using Leopotam.Ecs;
using Project.Scripts.Tags;
using UnityEngine;
using Zenject;

namespace Project.Scripts.Level.Spawners
{
    public class PlayerSpawner : MonoBehaviour
    {
        [Inject] private EcsWorld _world;
        
        private void Start()
        {
            EcsEntity playerEntity = _world.NewEntity();
            playerEntity.Get<PlayerComponent>();
            ref var spawnComponent = ref playerEntity.Get<SpawnComponent>();
            
            spawnComponent.Position = transform;
            spawnComponent.Rotation = transform.rotation;
        }
    }
}