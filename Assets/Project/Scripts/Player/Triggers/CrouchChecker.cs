using System;
using Project.Scripts.Tags;
using UnityEngine;

namespace Project.Scripts.Player.Triggers
{
    public class CrouchChecker : MonoBehaviour
    {
        public bool IsCrouch { get; private set; }

        private void OnTriggerStay(Collider other)
        {
            if (!other.TryGetComponent<PlayerComponentProvider>(out var playerComponentProvider))
            {
                IsCrouch = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            IsCrouch = false;
        }
    }
}