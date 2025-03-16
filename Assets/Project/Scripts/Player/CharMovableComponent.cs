using System;
using UnityEngine;

namespace Project.Scripts.Move
{
    [Serializable]
    public struct CharMovableComponent
    {
        public CharacterController CharController;
        public float Speed;
    }
    
}