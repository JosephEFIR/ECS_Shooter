using Project.Scripts.Configs;
using UnityEngine;

namespace Project.Scripts.Core.Enemy.AI
{
    internal struct EnemySpawnComponent
    {
        public Vector3[] PatrolPoints;
        public EnemyUnitConfig UnitConfigOverride;
        public BaseEnemyView PrefabOverride;
    }
}
