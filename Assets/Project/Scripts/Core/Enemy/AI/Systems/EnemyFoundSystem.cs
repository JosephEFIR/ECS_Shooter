using Leopotam.Ecs;
using Project.Scripts.Player;
using UnityEngine;

namespace Project.Scripts.Core.Enemy.AI.Systems
{
    public class EnemyFoundSystem : IEcsRunSystem
    {
        private readonly EcsFilter<EnemyComponent> _filter = null;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var enemy = ref _filter.Get1(i);

                if (enemy.UnitConfig == null) continue;
                ref var config = ref enemy.UnitConfig;

                if (enemy.Position == null || enemy.TargetsBuffer == null) continue;

                int hits = Physics.OverlapSphereNonAlloc(
                    enemy.Position.position,
                    config.ViewRadius,
                    enemy.TargetsBuffer,
                    config.TargetMask);

                if (hits > 0)
                {
                    float bestDistance = float.MaxValue;
                    Transform bestTarget = null;

                    for (int j = 0; j < hits; j++)
                    {
                        Collider targetCollider = enemy.TargetsBuffer[j];
                        if (targetCollider == null) continue;
                        
                        PlayerView playerView = targetCollider.GetComponentInParent<PlayerView>();
                        if (playerView == null) continue;
                        
                        Transform playerTransform = playerView.transform;

                        Vector3 dir = playerTransform.position - enemy.Position.position;
                        float distance = dir.magnitude;

                        float angle = Vector3.Angle(enemy.Position.forward, dir);
                        if (angle > config.ViewAngle / 2f) continue;

                        if (Physics.Raycast(enemy.Position.position, dir.normalized, distance, config.ObstacleMask))
                            continue;

                        if (distance < bestDistance)
                        {
                            bestDistance = distance;
                            bestTarget = playerTransform;
                        }
                    }

                    if (bestTarget != null)
                    {
                        enemy.Target = bestTarget;
                        enemy.CanSeePlayer = true;
                        enemy.Aggroed = true;
                    }
                    else
                    {
                        enemy.CanSeePlayer = false;

                        if (enemy.Target != null && enemy.Target.GetComponentInParent<PlayerView>() != null)
                        {
                            enemy.Target = null;
                            enemy.Aggroed = false;
                        }
                    }
                }
                else
                {
                    enemy.CanSeePlayer = false;

                    if (enemy.Target != null && enemy.Target.GetComponentInParent<PlayerView>() != null)
                    {
                        enemy.Target = null;
                        enemy.Aggroed = false;
                    }
                }
            }
        }
    }
}