using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Leopotam.Ecs;
using Project.Scripts.Factory.Pool;

namespace Project.Scripts.Weapon.Bullet
{
    public class BulletView : MonoBehaviour
    {
        public EcsEntity Entity;
        private Rigidbody _rigidbody;
        private CancellationTokenSource _token;
        
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            
            Collider bulletCollider = GetComponent<Collider>();
            if (bulletCollider != null) bulletCollider.isTrigger = true;
        }
           
        private void OnTriggerEnter(Collider other)
        {
            if (!Entity.IsAlive()) return;
            if (other.gameObject.CompareTag("Bullet")) return;
            
            ref var bulletComp = ref Entity.Get<BulletComponent>();
            if (bulletComp.Owner != null)
            {
                if (other.transform == bulletComp.Owner || 
                    other.transform.IsChildOf(bulletComp.Owner))
                {
                    return;
                }
            }

            ReturnBullet();
        }

        private async UniTaskVoid BulletReturnInTime()
        {
            StopTick();
            _token = new CancellationTokenSource();
            try
            {
                await UniTask.Delay(5000, cancellationToken: _token.Token);
                if (this != null && gameObject.activeSelf && Entity.IsAlive())
                {
                    ReturnBullet();
                }
            }
            finally
            {
                _token?.Dispose();
                _token = null;
            }
        }
        
        private void ReturnBullet()
        {
            if (!Entity.IsAlive()) return;
            StopTick();
            
            Entity.Get<BulletTriggerEvent>();
            BulletPool pool = Entity.Get<BulletComponent>().BulletPool;
            
            _rigidbody.linearVelocity = Vector3.zero;
            _rigidbody.angularVelocity = Vector3.zero;
            
            pool.ReturnObject(this);
        }

        private void StopTick()
        {
            if (_token != null)
            {
                _token.Cancel();
                _token.Dispose();
                _token = null;
            }
        }
        
        private void OnEnable()
        {
            if (Entity.IsAlive())
            {
                BulletReturnInTime().Forget();
            }
        }

        private void OnDisable()
        {
            StopTick();
        }

        private void OnDestroy()
        {
            StopTick();
        }
    }
}