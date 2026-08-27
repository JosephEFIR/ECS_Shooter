using Leopotam.Ecs;
using UnityEngine;

namespace Project.Scripts.Core.Enemy.Turret
{
    public class TurretAimSystem : IEcsRunSystem
    {
        private readonly EcsFilter<TurretComponent> _filter = null;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var turretComponent = ref _filter.Get1(i);
                ref var config = ref turretComponent.Config;
                ref var target = ref turretComponent.Target;
                ref var turretHead = ref turretComponent.Head;
                ref var canSeePlayer = ref turretComponent.CanSeePlayer;
                ref var initialUp = ref turretComponent.InitialUp;

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