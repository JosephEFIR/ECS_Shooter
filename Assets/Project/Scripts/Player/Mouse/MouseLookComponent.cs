using System;
using Unity.Cinemachine;
using UnityEngine;

namespace Project.Scripts.Move
{
    [Serializable]
    public struct MouseLookComponent
    {
        [HideInInspector]
        public Vector3 Direction;
        
        public float Sensitivity;
        public Camera Camera;
    }
}