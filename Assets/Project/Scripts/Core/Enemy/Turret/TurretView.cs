using UnityEngine;
using Leopotam.Ecs;
using Project.Scripts.Core.Enemy.AI;
using Project.Scripts.UI.Health;
using Project.Scripts.Weapon.Bullet;

namespace Project.Scripts.Core.Enemy.Turret
{
    public sealed class TurretView : BaseEnemyView
    {
        [SerializeField] private Transform bulletSpawnPoint;
        [SerializeField] private HealthUIView healthUIView;
        [SerializeField] private BulletView bulletView;
        [SerializeField] private Transform turretHad;
        
        public Transform BulletSpawnPoint => bulletSpawnPoint;
        public HealthUIView HealthUIView => healthUIView;
        public Transform TurretHad => turretHad;
        public BulletView BulletView => bulletView;
        
        
        protected override void Start()
        {
            base.Start();
            entity.Get<TurretComponent>();
        }
    }
}