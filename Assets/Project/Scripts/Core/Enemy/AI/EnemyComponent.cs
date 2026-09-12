using Project.Scripts.Configs;
using Project.Scripts.Core.Common;
using Project.Scripts.Factory.Pool;
using Project.Scripts.Weapon;
using UnityEngine;
using UnityEngine.AI;

namespace Project.Scripts.Core.Enemy.AI
{
    internal struct EnemyComponent
    {
        public BaseEnemyView View;
        public BaseEnemyConfig UnitConfig;
        public Transform Position;
        public HitBoxObserver HitBoxObserver;
        public AudioSource Audio;

        public EEnemyState State;
        public Transform Target;
        public Collider[] TargetsBuffer;
        public bool Aggroed;
        public bool CanSeePlayer;
        public bool CanAttack;

        public float DistanceToTarget;
        public float LookAroundTimer;
        public float LookAroundBaseYaw;
        
        public float NextAttackTime;
        public BulletPool BulletPool;
        public GameObject BulletPoolRoot;
    }
}
