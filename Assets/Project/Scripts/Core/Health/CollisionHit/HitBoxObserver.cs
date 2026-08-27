using Leopotam.Ecs;
using UnityEngine;

namespace Project.Scripts.Core.Common
{
    //[RequireComponent(typeof(Collider))]
    public class HitBoxObserver : MonoBehaviour
    {
        public EcsEntity Entity;
        private Collider _object;
        
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.layer != LayerMask.NameToLayer("Room"))
            {
                _object = collision.gameObject.GetComponent<Collider>();
                ref var hitboxComp = ref Entity.Get<HitBoxComponent>();
                
                hitboxComp.ColliderEntered = _object;
                Entity.Get<CollisionEnterEvent>();
            }
        }
    }
}