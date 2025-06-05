using UnityEngine;

namespace Project.Scripts.Move
{
    internal struct PlayerMovableComponent
    {
        public Rigidbody Rigidbody;
        
        //OnlyMove
        public int Speed;
        public int RunSpeed;
        public float Gravity;
        public Vector3 Velocity;
        public bool IsRun;
        //Jump
        public LayerMask GroundLayer;
        public bool IsGrounded;
        public float GroundDistance;
        public Transform GroundCheckTransform;
        public float JumpForce;
    }
    
}