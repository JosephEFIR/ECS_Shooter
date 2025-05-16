using System;
using Project.Scripts.Configs;
using UnityEngine;
using UnityEngine.Serialization;

namespace Project.Scripts.Tags
{
    [Serializable]
    public struct PlayerComponent
    {
        public PlayerConfig Config;
        
        public Camera camera;
        public CharacterController CharacterController;
        public Animator Animator;
        public Transform Position;
        public Transform GroundCheck;
    }
}