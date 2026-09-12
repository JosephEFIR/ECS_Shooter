using Leopotam.Ecs;
using UnityEngine;
using Random = UnityEngine.Random;
using System.Collections.Generic;
using Project.Scripts.Player;

namespace Project.Scripts.Core.Enemy.AI.Systems
{
    public class AiPatrolSystem : IEcsRunSystem
    {
        private readonly EcsFilter<EnemyUnitComponent, EnemyComponent, PatrolComponent> _filter = null;
        
        // Кэш для занятых точек (чтобы враги не толпились)
        private static Dictionary<Transform, float> _occupiedPoints = new Dictionary<Transform, float>();
        private static float _occupyTime = 3f; // Время, через которое точка освобождается
        
        public void Run()
        {
            // Обновляем занятые точки (удаляем устаревшие)
            List<Transform> toRemove = new List<Transform>();
            foreach (var kvp in _occupiedPoints)
            {
                if (Time.time > kvp.Value + _occupyTime)
                {
                    toRemove.Add(kvp.Key);
                }
            }
            foreach (var key in toRemove)
            {
                _occupiedPoints.Remove(key);
            }
            
            foreach (var i in _filter)
            {
                ref var enemyUnitComp = ref _filter.Get1(i);
                ref var enemyComp = ref _filter.Get2(i);
                ref var patrolComp = ref _filter.Get3(i);
                
                if (patrolComp.Points == null || patrolComp.Points.Length == 0)
                {
                    Debug.LogWarning($"AiPatrolSystem: у {enemyUnitComp.Agent?.name} нет патрульных точек!");
                    continue;
                }
            
                var agent = enemyUnitComp.Agent;
                if (agent == null || !agent.isOnNavMesh)
                {
                    continue;
                }
                
                var agentPos = agent.transform.position;
                
                // ⚠️ НЕ СБРАСЫВАЕМ ЦЕЛЬ, если она занята поиском игрока
                // Проверяем, является ли цель игроком
                bool isTargetPlayer = enemyComp.Target != null && 
                                     enemyComp.Target.GetComponent<PlayerView>() != null;
                
                
                if (isTargetPlayer)
                {
                    continue; // Игрок найден - патруль не нужен
                }
                
                // Проверяем, достигли ли мы текущей цели
                bool needNewPoint = enemyComp.Target == null;
                
                if (!needNewPoint && enemyComp.Target != null)
                {
                    float distToTarget = Vector3.Distance(agentPos, enemyComp.Target.position);
                    enemyComp.DistanceToTarget = distToTarget;
                    
                    // Если цель - патрульная точка
                    bool reachedByPath = agent.hasPath && agent.remainingDistance <= agent.stoppingDistance + 0.2f;
                    bool reachedDirectly = distToTarget <= 0.7f;
                    
                    // Если точка занята другим врагом - тоже ищем новую
                    bool isPointOccupied = _occupiedPoints.ContainsKey(enemyComp.Target) && 
                                          _occupiedPoints[enemyComp.Target] > Time.time;
                    
                    if (reachedByPath || reachedDirectly || isPointOccupied)
                    {
                        needNewPoint = true;
                    }
                }
                
                // Выбираем новую точку
                if (needNewPoint)
                {
                    Transform newPoint = GetFreePatrolPoint(patrolComp.Points, enemyComp.Target);
                    
                    if (newPoint != null)
                    {
                        enemyComp.Target = newPoint;
                        agent.SetDestination(newPoint.position);
                        
                        // Помечаем точку как занятую
                        _occupiedPoints[newPoint] = Time.time;
                        
                        enemyComp.DistanceToTarget = Vector3.Distance(agentPos, newPoint.position);
                        
                        Debug.Log($"AiPatrolSystem: {agent.name} идёт к точке {newPoint.name}");
                    }
                }
            }
        }
        
        // Выбирает свободную патрульную точку
        private Transform GetFreePatrolPoint(Transform[] points, Transform currentTarget)
        {
            if (points.Length == 1)
                return points[0];
            
            // Собираем свободные точки
            List<Transform> freePoints = new List<Transform>();
            foreach (var point in points)
            {
                if (point == currentTarget) continue; // Не выбираем ту же точку
                
                bool isOccupied = _occupiedPoints.ContainsKey(point) && 
                                 _occupiedPoints[point] > Time.time;
                
                if (!isOccupied)
                {
                    freePoints.Add(point);
                }
            }
            
            // Если есть свободные - выбираем случайную из них
            if (freePoints.Count > 0)
            {
                return freePoints[Random.Range(0, freePoints.Count)];
            }
            
            // Если все заняты - берём случайную (всё равно лучше чем стоять)
            return points[Random.Range(0, points.Length)];
        }
    }
}