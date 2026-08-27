using Project.Scripts.Configs;
using Project.Scripts.Core.Common;
using Project.Scripts.Factory.Pool;
using Project.Scripts.UI.Weapon.Enemy;
using UnityEngine;

namespace Project.Scripts.Core.Enemy.Turret
{
    internal struct TurretComponent
    {
        public TurretView TurretView;
        public AudioSource Audio;
        
        public Transform Head;
        public Transform TurretTransform;
        public Transform BulletSpawnPoint;
        
        public TurretConfig Config;
        public HitBoxObserver HitBoxObserver;

        public bool CanSeePlayer;
        public BulletPool BulletPool;
        public Transform Target;
        public float NextFireTime;
        public Vector3 InitialUp;
        public Collider[] TargetsBuffer;

        public Quaternion InitialRotation;
        public float PatrolAngle;
        public float PatrolDirection;
        
        public bool Aggroed;
    }
}