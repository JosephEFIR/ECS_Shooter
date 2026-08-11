using Project.Scripts.Tags;
using UnityEngine;

namespace Project.Scripts.Player.Triggers
{
    public class GroundChecker : MonoBehaviour
    {
        public bool IsGround { get; private set; }

        private void OnTriggerStay(Collider other)
        {
            if (!other.TryGetComponent<PlayerView>(out var playerView))
            {
                IsGround = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            IsGround = false;
        }
    }
}