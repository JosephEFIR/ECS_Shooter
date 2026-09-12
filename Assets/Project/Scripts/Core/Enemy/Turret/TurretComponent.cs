using Project.Scripts.Configs;
using Project.Scripts.Core.Common;
using Project.Scripts.Factory.Pool;
using Project.Scripts.UI.Weapon.Enemy;
using UnityEngine;

namespace Project.Scripts.Core.Enemy.Turret
{
    internal struct TurretComponent
    {
        public Transform Head;
        public Transform BulletSpawnPoint;
        
        public HitBoxObserver HitBoxObserver;
        
        public Vector3 InitialUp;
        public Quaternion InitialRotation;
    }
}