using TMPro;
using UnityEngine;

namespace Project.Scripts.UI.Weapon
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class TotalAmmoView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI textMeshPro;
        public TextMeshProUGUI TextMeshPro => textMeshPro;
    }
}