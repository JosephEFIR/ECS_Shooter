using Project.Scripts.Factory.Pool;
using UniRx;
using UnityEngine;

namespace Project.Scripts.Weapon
{
    internal struct WeaponComponent
    {
        public bool active;
        public Transform Owner;
        
        public WeaponConfig Config;
        public Transform BulletSpawnPoint;
        public AudioSource Audio;
        public BulletPool BulletPool;
        
        public bool CanFire;
        public float FireRate;                     
        public ReactiveProperty<int> MagazineSize;
        public ReactiveProperty<int> TotalAmmo;
        public ReactiveProperty<float> ReloadTime;
        
        public bool isReload;
        public bool isFire;
        
        public Transform LeftHandIKTarget;
        public Transform RightHandIKTarget;
        public Transform RightHintIKTarget;
        public Transform LeftHintIKTarget;
    }
}