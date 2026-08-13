using UnityEngine;

namespace Project.Scripts.UI.Health
{
    public class HealthUIView : MonoBehaviour
    {
        [SerializeField] private HealthSystem tinyHealthSystem;
        public HealthSystem TinyHealthSystem => tinyHealthSystem;
    }
}