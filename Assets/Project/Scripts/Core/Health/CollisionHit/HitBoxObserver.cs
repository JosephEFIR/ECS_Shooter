using Leopotam.Ecs;
using UnityEngine;

namespace Project.Scripts.Core.Common
{
    public class HitBoxObserver : MonoBehaviour
    {
        public EcsEntity Entity;
        private Collider _object;
        
        private void OnTriggerEnter(Collider other)
        {
            if (!Entity.IsAlive()) return;
            if (other.gameObject.layer == LayerMask.NameToLayer("Room")) return;

            _object = other;
            ref var hitboxComp = ref Entity.Get<HitBoxComponent>();
            
            hitboxComp.ColliderEntered = _object;
            Entity.Get<CollisionEnterEvent>();
        }
    }
}