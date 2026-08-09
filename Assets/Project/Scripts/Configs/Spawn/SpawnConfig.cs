using Project.Scripts.Player;
using Project.Scripts.Weapon;
using Project.Scripts.Weapon.Bullet;
using UnityEngine;

namespace Project.Scripts.Configs.Spawn
{
    [CreateAssetMenu(fileName = "SpwnConfig", menuName = "Configs/Spawn/SpawnConfig")]
    public class SpawnConfig : ScriptableObject
    {
        [Space(10)]
        [SerializeField] private PlayerView playerPrefab;

        [Space(10)] 
        [SerializeField] private WeaponView weaponPrefab;
        
        [Space(10)] 
        [SerializeField] private BulletView bulletPrefab;
        
        public PlayerView PlayerPrefab => playerPrefab;
        public WeaponView WeaponPrefab => weaponPrefab;
        public BulletView BulletPrefab => bulletPrefab;
    }
}