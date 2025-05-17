using UnityEngine;

namespace Project.Scripts.Move
{
    internal struct PlayerMovableComponent
    {
        public CharacterController CharController;
        
        //OnlyMove
        public int Speed;
        public int RunSpeed;
        public float Gravity;
        public Vector3 Velocity;
        //Jump
        public LayerMask GroundLayer;
        public bool IsGrounded;
        public float GroundDistance;
        public Transform GroundCheckTransform;
        public float JumpForce;
    }
    
}