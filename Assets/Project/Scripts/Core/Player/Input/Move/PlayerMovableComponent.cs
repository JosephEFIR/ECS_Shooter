using Project.Scripts.Player.Triggers;
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
        public GroundChecker GroundChecker;
        public bool IsGrounded;
        public float JumpForce;
        //Crouch
        public bool IsCrouched;
        public CapsuleCollider Collider;
        public CrouchChecker CrouchChecker;
    }
    
}