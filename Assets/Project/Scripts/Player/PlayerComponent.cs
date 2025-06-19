using System;
using Project.Scripts.Configs;
using Project.Scripts.Player.Triggers;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Serialization;

namespace Project.Scripts.Tags
{
    [Serializable]
    public struct PlayerComponent
    {
        public PlayerConfig Config;
        [Space(20)]
        [Header("Cameras")]
        public Camera camera;
        public CinemachineCamera FPVCamera;
        public CinemachineCamera TPVCamera;
        [Header("Movement & animation")]
        public Rigidbody Rigidbody;
        public Animator Animator;
        public Transform Position;
        [Header("Colliders & Hitboxes")]
        public CapsuleCollider Collider;
        public CrouchChecker CrouchChecker;
        public GroundChecker GroundCheck;
    }
}