using Leopotam.Ecs;
using UnityEngine;

namespace Project.Scripts.Other
{
    sealed class CursorLockedSystem : IEcsInitSystem
    {
        public void Init()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}