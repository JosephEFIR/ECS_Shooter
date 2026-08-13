using Project.Scripts.UI.Health;
using UnityEngine;

namespace Project.Scripts.UI.Weapon
{
    public class UiView : MonoBehaviour
    {
        [SerializeField] private TotalAmmoView totalAmmoView;
        [SerializeField] private HealthUIView healthUI;
        public TotalAmmoView TotalAmmoView => totalAmmoView;
        public HealthUIView HealthUIView => healthUI;
    }
}