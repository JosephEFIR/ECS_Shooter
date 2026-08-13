using UnityEngine;

namespace Project.Scripts.UI.Weapon
{
    public class UiView : MonoBehaviour
    {
        [SerializeField] private TotalAmmoView totalAmmoView;
        public TotalAmmoView TotalAmmoView => totalAmmoView;
    }
}