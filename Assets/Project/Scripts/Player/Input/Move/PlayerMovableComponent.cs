using System;
using UnityEngine;

namespace Project.Scripts.Move
{
    [Serializable]
    public struct PlayerMovableComponent
    {
        public CharacterController CharController;
        public int Speed;
        public int RunSpeed;
        public Vector3 velocity;
        public float gravity;
    }
    
}