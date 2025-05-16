using UnityEngine;

namespace Project.Scripts.Configs
{
    [CreateAssetMenu(fileName = "PlayerConfig", menuName = "Configs/PlayerConfig")]
    public class PlayerConfig : ScriptableObject
    {
        [Header("Physic & move")]
        [Range(0, 10)]
        [SerializeField] private int speed;
        [Range(0, 20)]
        [SerializeField] private int runSpeed;
        [Range(0, 10)]
        [SerializeField] private float jumpPower;
        [SerializeField] private int gravity;
        
        [SerializeField] private LayerMask groundLayer;
        [Range(0, 2)]
        [SerializeField] private float groundDistance;
        
        
        [Header("Other")]
        [Range(0, 10)]
        [SerializeField] private float mouseSensitivity;
        
        public int Speed => speed;
        public int RunSpeed => runSpeed;
        public float JumpPower => jumpPower;
        public int Gravity => gravity;
        public float MouseSensitivity => mouseSensitivity;
        public LayerMask GroundLayer => groundLayer;
        public float GroundDistance => groundDistance;
    }
}

