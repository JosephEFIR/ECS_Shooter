using Leopotam.Ecs;
using Project.Scripts.Level.Spawners;
using UnityEngine;
using Zenject;

namespace Project.Scripts.Weapon
{
    public class WeaponSpawner : MonoBehaviour
    {
        [Inject] private EcsWorld _world;

        private void Start()
        {
            EcsEntity entity = _world.NewEntity();
            entity.Get<WeaponComponent>();
            ref var spawnComponent = ref entity.Get<SpawnComponent>();
            
            spawnComponent.Position = transform; 
            spawnComponent.Rotation = transform.rotation;
            spawnComponent.Parent = transform.parent;
        }
    }
}