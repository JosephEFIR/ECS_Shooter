using System;
using UnityEngine;

namespace Project.Scripts.Move
{
    [Serializable]
    public struct MouseLookComponent
    {
        public Vector3 Direction;
        public float Sensitivity;
        public Camera Camera;
    }
}