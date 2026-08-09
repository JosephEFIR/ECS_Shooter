using System.Collections.Generic;
using UnityEngine;

namespace Project.Scripts.Weapon
{
    public class WeaponView : MonoBehaviour
    {
        [SerializeField] private WeaponConfig config;
        [SerializeField] private Transform bulletSpawnPoint;
        
        [SerializeField] private Transform leftHandIKTarget;
        [SerializeField] private Transform rightHandIKTarget;
        [SerializeField] private Transform leftElbowIKTarget;
        [SerializeField] private Transform rightElbowIKTarget;

        public WeaponConfig Config => config;
        public Transform BulletSpawnPoint => bulletSpawnPoint;
        public Transform LeftHandIKTarget => leftHandIKTarget;
        public Transform RightHandIKTarget => rightHandIKTarget;
        public Transform LeftElbowIKTarget => leftElbowIKTarget;
        public Transform RightElbowIKTarget => rightElbowIKTarget;
        
    }
}