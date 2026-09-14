using Project.Scripts.Animation;
using Project.Scripts.Weapon;
using UnityEngine;
using UnityEngine.AI;

namespace Project.Scripts.Core.Enemy.AI
{
    internal struct EnemyUnitComponent
    {
        public WeaponHolder WeaponHolder;
        public Transform AimTarget;
        public AnimIK AnimIK;
        public bool IsAiming;
        public NavMeshAgent Agent;
        public Transform AimPivot;
        public Transform BulletSpawnPoint;
        
        public int PatrolIndex;
        public bool PatrolDestinationSet;
    }
}