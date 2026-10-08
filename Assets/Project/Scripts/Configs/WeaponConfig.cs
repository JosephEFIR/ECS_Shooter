using System.ComponentModel;
using Project.Scripts.Weapon.Bullet;
using UnityEngine;

namespace Project.Scripts.Weapon
{
    [CreateAssetMenu(fileName = "WeaponConfig", menuName = "Configs/WeaponConfig")]
    public class WeaponConfig : ScriptableObject
    {
        [SerializeField] private int totalAmmo;
        [SerializeField] private float fireRate;
        [Description("Fire rate bullets per minute")]
        [SerializeField] private int magazineSize;

        [Header("Reload (in seconds!)")]
        [SerializeField] private float reloadTime = 2f;
        [SerializeField] private float weaponReloadClipLength = 2f;
        [SerializeField] private float playerReloadClipLength = 3.3f;

        [Header("Bullet")] 
        [SerializeField] private BulletView bulletView;
        
        public int TotalAmmo => totalAmmo;
        public float FireRate => fireRate;
        public int MagazineSize => magazineSize;
        public float ReloadTime => reloadTime;
        public float WeaponReloadClipLength => weaponReloadClipLength;
        public float PlayerReloadClipLength => playerReloadClipLength;
        public BulletView BulletView => bulletView;
    }
}