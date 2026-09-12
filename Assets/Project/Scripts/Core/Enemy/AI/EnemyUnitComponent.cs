using Project.Scripts.Weapon;
using UnityEngine;
using UnityEngine.AI;

namespace Project.Scripts.Core.Enemy.AI
{
    internal struct EnemyUnitComponent
    {
        public WeaponHolder WeaponHolder;
        public NavMeshAgent Agent;
        public Transform AimPivot;
        public Transform BulletSpawnPoint;
        
        public int PatrolIndex;
        public bool PatrolDestinationSet;
    }
}