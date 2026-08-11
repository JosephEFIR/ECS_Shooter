using System.ComponentModel;
using Project.Scripts.Weapon.Bullet;
using UnityEngine;
using UnityEngine.Serialization;

namespace Project.Scripts.Weapon
{
    [CreateAssetMenu(fileName = "WeaponConfig", menuName = "Configs/WeaponConfig")]
    public class WeaponConfig : ScriptableObject
    {
        [SerializeField] private int totalAmmo;
        [SerializeField] private float fireRate;
        [Description("Fire rate bullets per minute")]
        [SerializeField] private int magazineSize;
        [Header("In seconds!")]
        [SerializeField] private int reloadTime;

        [Header("Bullet")] 
        [SerializeField] private BulletView bulletView;
        
        public int TotalAmmo => totalAmmo;
        public float FireRate => fireRate;
        public int MagazineSize => magazineSize;
        public int ReloadTime => reloadTime;
        public BulletView BulletView => bulletView;
    }
}