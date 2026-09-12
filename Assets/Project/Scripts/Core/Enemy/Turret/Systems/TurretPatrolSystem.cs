using Leopotam.Ecs;
using Project.Scripts.Core.Enemy.AI;
using UnityEngine;

namespace Project.Scripts.Core.Enemy.Turret
{
    public class TurretPatrolSystem : IEcsRunSystem
    {
        private readonly EcsFilter<EnemyComponent, TurretComponent, PatrolComponent> _filter = null;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var turret = ref _filter.Get2(i);
                ref var enemy = ref _filter.Get1(i);
                ref var patrolComp = ref _filter.Get3(i);
                
                if (enemy.Target != null) continue;
                
                patrolComp.PatrolAngle += patrolComp.PatrolDirection * patrolComp.PatrolSpeed * Time.deltaTime;

                if (Mathf.Abs(patrolComp.PatrolAngle) >= patrolComp.PatrolRange)
                {
                    patrolComp.PatrolAngle = Mathf.Clamp(patrolComp.PatrolAngle, -patrolComp.PatrolRange, patrolComp.PatrolRange);
                    patrolComp.PatrolDirection *= -1f;
                }

                Quaternion targetRotation = turret.InitialRotation * Quaternion.AngleAxis(patrolComp.PatrolAngle, turret.Head.up);
                turret.Head.rotation = Quaternion.RotateTowards(turret.Head.rotation, targetRotation, patrolComp.PatrolSpeed * Time.deltaTime);
            }
        }
    }
}