using System;
using UnityEngine;

namespace Project.Scripts.Move
{
    [Serializable]
    public struct MovableComponent
    {
        public CharacterController CharController;
        public float Speed;
    }
    
}