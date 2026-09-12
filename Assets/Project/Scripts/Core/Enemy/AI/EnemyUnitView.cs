using Leopotam.Ecs;
using Project.Scripts.Animation;
using Project.Scripts.Weapon;
using UnityEngine;
using UnityEngine.AI;

namespace Project.Scripts.Core.Enemy.AI
{
    public class EnemyUnitView : BaseEnemyView
    {
        [SerializeField] private NavMeshAgent agent;
        [SerializeField] private Animator animator;
        [SerializeField] private AnimIK animik;
        [SerializeField] private WeaponHolder weaponHolder;
        
        public NavMeshAgent Agent => agent;
        public Animator Animator => animator;
        public WeaponHolder WeaponHolder => weaponHolder;
        public AnimIK Animik => animik;
    }
}