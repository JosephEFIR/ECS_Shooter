using Project.Scripts.Factory.Pool;
using UniRx;
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
        public ReactiveProperty<int> MagazineSize;
        public ReactiveProperty<int> TotalAmmo;
        public ReactiveProperty<float> ReloadTime;
        
        public Transform LeftHandIKTarget;
        public Transform RightHandIKTarget;
    }
}