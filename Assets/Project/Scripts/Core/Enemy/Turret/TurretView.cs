using UnityEngine;
using Leopotam.Ecs;
using Project.Scripts.Configs;
using Project.Scripts.Core.Common;
using Project.Scripts.Factory.Pool;
using Project.Scripts.UI.Health;
using Project.Scripts.UI.Weapon.Enemy;
using Project.Scripts.Weapon.Bullet;
using Zenject;

namespace Project.Scripts.Core.Enemy.Turret
{
    public class TurretView : MonoBehaviour
    {
        [Inject] private EcsWorld _world;
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private TurretConfig turretConfig;
        [SerializeField] private BulletView bulletView;
        [SerializeField] private Transform bulletSpawnPoint;
        [SerializeField] private HealthUIView healthUIView;
        [SerializeField] private HitBoxObserver hitBoxObserver;
        [SerializeField] private Transform turretHad;
        
        
        public AudioSource AudioSource => audioSource;
        public TurretConfig TurretConfig => turretConfig;
        public BulletView BulletView => bulletView;
        public Transform BulletSpawnPoint => bulletSpawnPoint;
        public HealthUIView HealthUIView => healthUIView;
        public HitBoxObserver HitBoxObserver => hitBoxObserver;
        public Transform TurretHad => turretHad;
        
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
            EcsEntity entity = _world.NewEntity();
            ref var turretComponent = ref entity.Get<TurretComponent>();
            turretComponent.TurretView = this;
            entity.Get<InitComponent>();
        }
    }
}