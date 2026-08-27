using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Project.Scripts.Factory.Pool
{
    public class ObjectPool<TObject> where TObject : Component
    {
        private Queue<TObject> _pool = new();
        public TObject Prefab;

        private Vector3 _pos = Vector3.zero;
        private Transform _parent = null;

        public void CreatePool(int poolSize, Transform position, Transform parent)
        {
            _pos = position != null ? position.position : Vector3.zero;
            _parent = parent;
            _pool = new Queue<TObject>(poolSize);
            
            for (int i = 0; i < poolSize; i++)
            {
                TObject obj = Object.Instantiate(Prefab, _pos, Quaternion.identity, _parent);
                obj.gameObject.SetActive(false);
                _pool.Enqueue(obj);
            }
        }
        
        public TObject GetObject()
        {
            TObject obj;
            
            if (_pool.Count > 0)
            {
                obj = _pool.Dequeue();
            }
            else
            {
                obj = Object.Instantiate(Prefab, _pos, Quaternion.identity, _parent);
            }
            
            obj.gameObject.SetActive(true);
            obj.transform.parent = null;
            return obj;
        }

        public void ReturnObject(TObject obj)
        {
            if (obj == null) return;
            
            obj.gameObject.SetActive(false);
            if (_parent != null)
            {
                obj.transform.parent = _parent;
            }
            _pool.Enqueue(obj);
        }
    }
}