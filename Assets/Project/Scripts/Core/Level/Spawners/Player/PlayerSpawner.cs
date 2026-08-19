using Leopotam.Ecs;
using Project.Scripts.Tags;
using UnityEngine;
using Zenject;

namespace Project.Scripts.Level.Spawners
{
    public class PlayerSpawner : MonoBehaviour
    {
        [Inject] private EcsWorld _world;
        
        private void Awake()
        {
            if (_world == null)
            {
                var sceneContext = FindObjectOfType<SceneContext>();
                if (sceneContext != null)
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
            EcsEntity playerEntity = _world.NewEntity();
            playerEntity.Get<PlayerComponent>();
            ref var spawnComponent = ref playerEntity.Get<SpawnComponent>();
            
            spawnComponent.Position = transform;
            spawnComponent.Rotation = transform.rotation;
        }
    }
}