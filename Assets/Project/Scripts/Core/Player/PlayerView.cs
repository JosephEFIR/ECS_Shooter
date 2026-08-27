using Project.Scripts.Animation;
using Project.Scripts.Configs;
using Project.Scripts.Core.Common;
using Project.Scripts.Core.Player;
using Project.Scripts.Player.Triggers;
using Project.Scripts.Weapon;
using Unity.Cinemachine;
using UnityEngine;

namespace Project.Scripts.Player
{
    public class PlayerView : MonoBehaviour
    {
        [SerializeField] private PlayerConfig config;
        [Space(20)]
        
        [Header("Cameras")]
        [SerializeField] private CinemachineCamera fpvCamera;
        [SerializeField] private CinemachineCamera tpvCamera;
        
        [Header("Movement & animation")]
        [SerializeField] private Rigidbody rigidbody;
        [SerializeField] private Animator animator;
        [SerializeField] private AnimIK animIK;
        [SerializeField] private Transform position;
        
        [Header("Colliders & Hitboxes")]
        [SerializeField] private HitBoxObserver hitBoxObserver;
        [SerializeField] private CapsuleCollider collider;
        [SerializeField] private CrouchChecker crouchChecker;
        [SerializeField] private GroundChecker groundChecker;

        [Header("Weapon")] 
        [SerializeField] private WeaponHolder weaponHolder;
        [SerializeField] private Transform aimTarget;
        
        
        public PlayerConfig Config => config;
        public CinemachineCamera FPVCamera => fpvCamera;
        public CinemachineCamera TPVCamera => tpvCamera;
        public Rigidbody Rigidbody => rigidbody;
        public Animator Animator => animator;
        public Transform Position => position;
        public HitBoxObserver HitBoxObserver => hitBoxObserver;
        public CapsuleCollider Collider => collider;
        public CrouchChecker CrouchChecker => crouchChecker;
        public GroundChecker GroundChecker => groundChecker;
        public WeaponHolder WeaponHolder => weaponHolder;
        public Transform AimTarget => aimTarget;
        public AnimIK AnimIK => animIK;
    }
}