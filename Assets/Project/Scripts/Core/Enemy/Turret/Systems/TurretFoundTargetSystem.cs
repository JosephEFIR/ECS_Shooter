using Leopotam.Ecs;
using UnityEngine;

namespace Project.Scripts.Core.Enemy.Turret
{
    public class TurretFoundTargetSystem : IEcsRunSystem
    {
        private readonly EcsFilter<TurretComponent> _filter = null;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var turretComponent = ref _filter.Get1(i);
                ref var config = ref turretComponent.Config;
                ref var target = ref turretComponent.Target;
                ref var targetsBuffer = ref turretComponent.TargetsBuffer;
                ref var turretHead = ref turretComponent.Head;
                ref var canSeePlayer = ref turretComponent.CanSeePlayer;
                
                if (turretComponent.Aggroed)
                {
                    if (target != null)
                    {
                        float distanceToTarget = Vector3.Distance(turretHead.position, target.position);
                        if (distanceToTarget > config.ViewRadius)
                        {
                            turretComponent.Aggroed = false;
                            target = null;
                            canSeePlayer = false;
                        }
                        else
                        {
                            continue;
                        }
                    }
                    else
                    {
                        turretComponent.Aggroed = false;
                        target = null;
                        canSeePlayer = false;
                    }
                }
                
                target = null;
                canSeePlayer = false;

                int hits = Physics.OverlapSphereNonAlloc(turretHead.position, config.ViewRadius, targetsBuffer, config.TargetMask);
                if (hits > 0)
                {
                    float bestDistance = float.MaxValue;
                    Transform bestTarget = null;

                    for (int j = 0; j < hits; j++)
                    {
                        Collider targetCollider = targetsBuffer[j];

                        if (targetCollider == null) continue;
                        if (!targetCollider.CompareTag("Player")) continue;

                        Vector3 directionToTarget = targetCollider.transform.position - turretHead.position;
                        float distanceToTarget = directionToTarget.magnitude;

                        float angleToTarget = Vector3.Angle(turretHead.forward, directionToTarget);
                        if (angleToTarget > config.ViewAngle / 2) continue;

                        if (Physics.Raycast(turretHead.position, directionToTarget.normalized, distanceToTarget, config.ObstacleMask))
                            continue;

                        if (distanceToTarget < bestDistance)
                        {
                            bestDistance = distanceToTarget;
                            bestTarget = targetCollider.transform;
                        }
                    }

                    target = bestTarget;
                }
                
                if (target == null)
                {
                    ref var patrolAngle = ref turretComponent.PatrolAngle;
                    ref var patrolDirection = ref turretComponent.PatrolDirection;
                    ref var initialRotation = ref turretComponent.InitialRotation;

                    float patrolSpeed = config.PatrolSpeed;
                    float patrolRange = config.PatrolRange;

                    patrolAngle += patrolDirection * patrolSpeed * Time.deltaTime;

                    if (Mathf.Abs(patrolAngle) >= patrolRange)
                    {
                        patrolAngle = Mathf.Clamp(patrolAngle, -patrolRange, patrolRange);
                        patrolDirection *= -1f;
                    }

                    Quaternion targetRotation = initialRotation * Quaternion.AngleAxis(patrolAngle, turretHead.up);
                    turretHead.rotation = Quaternion.RotateTowards(turretHead.rotation, targetRotation, patrolSpeed * Time.deltaTime);
                }
            }
        }
    }
}