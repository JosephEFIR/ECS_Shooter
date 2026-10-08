using UnityEngine;

namespace Project.Scripts.Weapon
{
    public class WeaponIkTargets : MonoBehaviour
    {
        [SerializeField] private Transform rightHand;
        [SerializeField] private Transform rightElbow;
        [SerializeField] private Transform leftHand;
        [SerializeField] private Transform leftElbow;

        public Transform RightHand => rightHand;
        public Transform RightElbow => rightElbow;
        public Transform LeftHand => leftHand;
        public Transform LeftElbow => leftElbow;
    }
}