using UnityEngine;

namespace Project.Scripts.Configs
{
    [CreateAssetMenu(fileName = "TurretConfig", menuName = "Configs/TurretConfig")]
    public class TurretUnitConfig : BaseEnemyConfig
    {
        [Header("Patrol")]
        [SerializeField] private float patrolSpeed = 30f;   
        [SerializeField] private float patrolRange = 90f; 
        
        public float PatrolSpeed => patrolSpeed;
        public float PatrolRange => patrolRange;
    }
}