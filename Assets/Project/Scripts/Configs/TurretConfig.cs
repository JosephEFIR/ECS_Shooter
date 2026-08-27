using UnityEngine;

namespace Project.Scripts.Configs
{
    [CreateAssetMenu(fileName = "TurretConfig", menuName = "Configs/TurretConfig")]
    public class TurretConfig : ScriptableObject
    {
        [Header("View")]
        [SerializeField] private float viewRadius = 15f;
        [Range(0, 360)] [SerializeField] private float viewAngle = 90f;
        [Range(0, 180)] [SerializeField] private float verticalAngle = 90f;
        [SerializeField] private float rotationSpeed = 150f;
            
        [Header("Target")]
        [SerializeField] private LayerMask targetMask;
        [SerializeField] private LayerMask obstacleMask;
        
        [Header("Patrol")]
        [SerializeField] private float patrolSpeed = 30f;   
        [SerializeField] private float patrolRange = 90f; 
        
        [Header("Fire")]
        [SerializeField] private float fireRate = 1f;
        [SerializeField] private float bulletSpeed = 20f; 
        [SerializeField] private int poolSize = 20;
        [SerializeField] private float aimThreshold = 10f;

        [Header("Health")]
        [SerializeField] private int health = 100;
        
        
        public float ViewRadius => viewRadius;
        public float ViewAngle => viewAngle;
        public float VerticalAngle => verticalAngle;
        public LayerMask TargetMask => targetMask;
        public LayerMask ObstacleMask => obstacleMask;
        public float PatrolSpeed => patrolSpeed;
        public float PatrolRange => patrolRange;
        public float RotationSpeed => rotationSpeed;
        public float FireRate => fireRate;
        public float BulletSpeed => bulletSpeed;
        public int PoolSize => poolSize;
        public float AimThreshold => aimThreshold;
        public int Health => health;
    }
}