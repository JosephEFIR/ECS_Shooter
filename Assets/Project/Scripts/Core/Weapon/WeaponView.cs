using System.Collections.Generic;
using UnityEngine;

namespace Project.Scripts.Weapon
{
    public class WeaponView : MonoBehaviour
    {
        [SerializeField] private WeaponConfig config;
        [SerializeField] private Transform bulletSpawnPoint;
        [SerializeField] private AudioSource audio;

        [SerializeField] private Transform leftHandIKTarget;
        [SerializeField] private Transform rightHandIKTarget;
        [SerializeField] private Transform leftHintIKTarget;
        [SerializeField] private Transform rightHintIKTarget;
        

        public WeaponConfig Config => config;
        public Transform BulletSpawnPoint => bulletSpawnPoint;
        public AudioSource Audio => audio;
        public Transform LeftHandIKTarget => leftHandIKTarget;
        public Transform RightHandIKTarget => rightHandIKTarget;
        public Transform LeftHintIKTarget => leftHintIKTarget;
        public Transform RightHintIKTarget => rightHintIKTarget;
    }
}