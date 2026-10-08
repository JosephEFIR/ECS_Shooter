using UnityEngine;

namespace Project.Scripts.Weapon
{
    public class WeaponMagazine : MonoBehaviour
    {
        [SerializeField] private Transform magazine;
        [SerializeField] private Transform attachedPoint;
        [SerializeField] private Vector3 handLocalPosition = Vector3.zero;
        [SerializeField] private Vector3 handLocalRotation = Vector3.zero;

        private Transform _playerLeftHand;

        public void SetPlayerLeftHand(Transform leftHand)
        {
            _playerLeftHand = leftHand;
            Debug.Log($"[Magazine] SetPlayerLeftHand: {(leftHand != null ? leftHand.name : "NULL")}");
        }

        public void MoveToLeftHand()
        {
            Debug.Log($"[Magazine] MoveToLeftHand called, _playerLeftHand = {(_playerLeftHand != null ? _playerLeftHand.name : "NULL")}");
            if (_playerLeftHand == null) return;

            magazine.SetParent(_playerLeftHand, false);
            magazine.localPosition = handLocalPosition;
            magazine.localRotation = Quaternion.Euler(handLocalRotation);
            Debug.Log($"[Magazine] moved to hand, parent = {magazine.parent.name}");
        }

        public void Attach()
        {
            Debug.Log($"[Magazine] Attach called");
            magazine.SetParent(attachedPoint, false);
            magazine.localPosition = Vector3.zero;
            magazine.localRotation = Quaternion.identity;
        }
    }
}