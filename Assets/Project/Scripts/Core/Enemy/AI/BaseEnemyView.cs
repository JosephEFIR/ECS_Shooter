using Leopotam.Ecs;
using Project.Scripts.Configs;
using Project.Scripts.Core.Common;
using UnityEngine;
using Zenject;

namespace Project.Scripts.Core.Enemy.AI
{
    public abstract class BaseEnemyView : MonoBehaviour
    {
        [Inject] protected EcsWorld _world;
        protected EcsEntity entity;
        
        [SerializeField] private BaseEnemyConfig config;
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private HitBoxObserver hitBoxObserver;
        
        public BaseEnemyConfig Config => config;
        public AudioSource AudioSource => audioSource;
        public HitBoxObserver HitBoxObserver => hitBoxObserver;

        
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
        
        protected virtual void Start()
        {
            entity = _world.NewEntity();
            ref var enemyComp = ref entity.Get<EnemyComponent>();
            enemyComp.View = this;
            entity.Get<InitComponent>();
        }
    }
}