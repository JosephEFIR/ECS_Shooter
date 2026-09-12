using Project.Scripts.Weapon;
using Project.Scripts.Weapon.Bullet;
using UnityEngine;

namespace Project.Scripts.Configs
{
    [CreateAssetMenu(fileName = "EnemyConfig", menuName = "Configs/EnemyConfig")]
    public class EnemyUnitConfig : BaseEnemyConfig
    {

        [Header("Weapon")]
        [SerializeField] private WeaponView weapon;
        
        public WeaponView Weapon => weapon;
    }
}
