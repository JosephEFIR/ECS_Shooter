using Project.Scripts.Factory.Pool;
using UnityEngine;

namespace Project.Scripts.Weapon.Bullet
{
    internal struct BulletComponent
    {
        public Transform Owner;
        public BulletPool BulletPool;
    }
}