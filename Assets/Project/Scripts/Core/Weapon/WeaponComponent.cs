using Project.Scripts.Factory.Pool;
using UnityEngine;

namespace Project.Scripts.Weapon
{
    internal struct WeaponComponent
    {
        public bool active;
        
        public WeaponConfig Config;
        public Transform BulletSpawnPoint;
        public BulletPool BulletPool;
        
        public bool isReload;
        public bool isFire;
        
        public bool CanFire;
        public float FireRate;                     
        public int MagazineSize;
        public int TotalAmmo;
        public float ReloadTime;
        
        public Transform LeftHandIKTarget;
        public Transform RightHandIKTarget;
    }
}