using Project.Scripts.Core.Enemy.AI;
using Project.Scripts.Player;
using Project.Scripts.Weapon;
using Project.Scripts.Weapon.Bullet;
using UnityEngine;
using UnityEngine.Serialization;

namespace Project.Scripts.Configs.Spawn
{
    [CreateAssetMenu(fileName = "SpwnConfig", menuName = "Configs/Spawn/SpawnConfig")]
    public class SpawnConfig : ScriptableObject
    {
        [Space(10)]
        [SerializeField] private PlayerView playerPrefab;
        
        [FormerlySerializedAs("enemyPrefab")]
        [Space(10)]
        [SerializeField] private BaseEnemyView enemyPrefab;

        [Space(10)] 
        [SerializeField] private WeaponView weaponPrefab;
        
        [Space(10)] 
        [SerializeField] private BulletView bulletPrefab;
        
        public PlayerView PlayerPrefab => playerPrefab;
        public BaseEnemyView EnemyPrefab => enemyPrefab;
        public WeaponView WeaponPrefab => weaponPrefab;
        public BulletView BulletPrefab => bulletPrefab;
    }
}