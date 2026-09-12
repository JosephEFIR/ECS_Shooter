using Project.Scripts.Weapon.Bullet;
using UnityEngine;

namespace Project.Scripts.Configs
{
    public enum EEnemyType
    {
        Melee,
        Ranged
    }
    
    public class BaseEnemyConfig : ScriptableObject
    {
        [Header("Type")]
        [SerializeField] private EEnemyType enemyType = EEnemyType.Melee;

        [Header("Move")]
        [SerializeField] private float moveSpeed = 3.5f;
        [SerializeField] private float angularSpeed = 120f;
        [SerializeField] private float acceleration = 8f;

        [Header("Health")]
        [SerializeField] private int health = 50;

        [Header("View")]
        [SerializeField] private float viewRadius = 12f;
        [Range(0, 360)] [SerializeField] private float viewAngle = 90f;
        [Range(0, 180)] [SerializeField] private float verticalAngle = 90f;
        [SerializeField] private LayerMask targetMask;
        [SerializeField] private LayerMask obstacleMask;
        
        [Header("Aim")]
        [SerializeField] private float rotationSpeed = 180f;
        [SerializeField] private float aimThreshold = 12f;

        [Header("Melee")]
        [SerializeField] private float attackSpeed = 1f;
        [SerializeField] private float meleeRange = 1.8f;
        [SerializeField] private float meleeDamage = 8f;

        [Header("Ranged")]
        [SerializeField] private float fireRate = 2f;
        [SerializeField] private float bulletSpeed = 40f;
        [SerializeField] private float minDistanceToPlayer = 6f;
        [SerializeField] private float maxDistanceToPlayer = 14f;
        [SerializeField] private int poolSize = 16;
        [SerializeField] private BulletView bulletView;

        [Header("Patrol")]
        [SerializeField] private float lookAroundDuration = 3f;
        [SerializeField] private float lookAroundAngle = 50f;
        [SerializeField] private float lookAroundSpeed = 80f;
        [SerializeField] private float waypointReachDistance = 0.6f;

        public EEnemyType EnemyType => enemyType;
        public float MoveSpeed => moveSpeed;
        public float AngularSpeed => angularSpeed;
        public float Acceleration => acceleration;
        public int Health => health;
        public float ViewRadius => viewRadius;
        public float ViewAngle => viewAngle;
        public LayerMask TargetMask => targetMask;
        public LayerMask ObstacleMask => obstacleMask;
        public float RotationSpeed => rotationSpeed;
        public float AimThreshold => aimThreshold;
        public float AttackSpeed => attackSpeed;
        public float MeleeRange => meleeRange;
        public float MeleeDamage => meleeDamage;
        public float FireRate => fireRate;
        public float BulletSpeed => bulletSpeed;
        public float MinDistanceToPlayer => minDistanceToPlayer;
        public float MaxDistanceToPlayer => maxDistanceToPlayer;
        public int PoolSize => poolSize;
        public BulletView BulletView => bulletView;
        public float LookAroundDuration => lookAroundDuration;
        public float LookAroundAngle => lookAroundAngle;
        public float LookAroundSpeed => lookAroundSpeed;
        public float WaypointReachDistance => waypointReachDistance;
    }
}