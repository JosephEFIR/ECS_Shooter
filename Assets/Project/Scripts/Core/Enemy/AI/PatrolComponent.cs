using UnityEngine;

namespace Project.Scripts.Core.Enemy.AI
{
    internal struct PatrolComponent
    {
        public Transform[] Points;
        public float PatrolRange;
        public float PatrolAngle;
        public float PatrolDirection;
        public float PatrolSpeed;
    }
}