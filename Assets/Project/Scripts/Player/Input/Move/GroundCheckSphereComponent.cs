using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Project.Scripts.Move
{
    [Serializable]
    public struct GroundCheckSphereComponent
    {
        public LayerMask mask;
        public bool isGrounded;
        public float groundDistance;
        public Transform groundCheckSphere;
    }
}