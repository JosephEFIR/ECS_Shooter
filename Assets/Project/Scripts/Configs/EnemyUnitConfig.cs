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
        
        public float AttackRange;   // дистанция атаки (например, 10-15)
        public float RotationSpeed; // скорость поворота (у турели уже есть)
        public float FireRate;      // выстрелов в секунду
        public float BulletSpeed;   // скорость пули
        
        public WeaponView Weapon => weapon;
    }
}
