using System;
using UnityEngine;

namespace Project.Scripts.Move
{
    [Serializable]
    public struct DirectionComponent
    {
        [HideInInspector]
        public Vector3 Direction;
    }
}