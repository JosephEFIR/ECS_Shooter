using Project.Scripts.Animation;
using Project.Scripts.Configs;
using Project.Scripts.Player;
using Project.Scripts.Player.Triggers;
using Project.Scripts.Weapon;
using Unity.Cinemachine;
using UnityEngine;

namespace Project.Scripts.Tags
{
    internal struct PlayerComponent
    {
        public PlayerConfig Config;
        
        public Camera camera;
        public CinemachineCamera FPVCamera;
        public CinemachineCamera TPVCamera;
        
        public Rigidbody Rigidbody;
        public Animator Animator;
        public AnimIK AnimIK;
        public Transform Position; 
  
        public CapsuleCollider Collider;
        public CrouchChecker CrouchChecker;
        public GroundChecker GroundChecker;

        public WeaponHolder WeaponHolder;
        public Transform AimTarget;
    }
}