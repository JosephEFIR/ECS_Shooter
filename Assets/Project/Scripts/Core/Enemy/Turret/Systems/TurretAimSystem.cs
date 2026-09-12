using Leopotam.Ecs;
using Project.Scripts.Core.Enemy.AI;
using UnityEngine;

namespace Project.Scripts.Core.Enemy.Turret
{
    public class TurretAimSystem : IEcsRunSystem
    {
        private readonly EcsFilter<EnemyComponent, TurretComponent> _filter = null;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var turretComponent = ref _filter.Get2(i);
                ref var enemyComp = ref _filter.Get1(i);
                ref var config = ref enemyComp.UnitConfig;
                ref var turretHead = ref turretComponent.Head;
                ref var initialUp = ref turretComponent.InitialUp;
                ref var target = ref enemyComp.Target;
                ref var canSeePlayer = ref enemyComp.CanSeePlayer;
                
                if (target == null)
                {
                    canSeePlayer = false;
                    continue;
                }

                Vector3 direction = target.position - turretHead.position;
                Quaternion targetRotation = Quaternion.LookRotation(direction, initialUp);
                turretHead.rotation = Quaternion.RotateTowards(turretHead.rotation, targetRotation, config.RotationSpeed * Time.deltaTime);

                float angleToTarget = Vector3.Angle(turretHead.forward, direction);
                canSeePlayer = angleToTarget < config.AimThreshold;
            }
        }
    }
}