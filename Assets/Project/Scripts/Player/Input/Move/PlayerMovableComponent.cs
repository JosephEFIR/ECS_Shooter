using UnityEngine;

namespace Project.Scripts.Move
{
    internal struct PlayerMovableComponent
    {
        public CharacterController CharController;
        
        public int Speed;
        public int RunSpeed;
        public float Gravity;
        public Vector3 Velocity;
    }
    
}