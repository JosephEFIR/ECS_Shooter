using UnityEngine;

namespace Project.Scripts.UI.Weapon.Enemy
{
    public class TurretHealthUIView : MonoBehaviour
    {
        [SerializeField] private HealthSystem tinyHealthSystem;
        public HealthSystem TinyHealthSystem => tinyHealthSystem;
    }
}