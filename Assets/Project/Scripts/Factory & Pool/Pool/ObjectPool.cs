using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Project.Scripts.Factory.Pool
{
    public class ObjectPool<TObject> where TObject : Component
    {
        private Queue<TObject> _pool = new();
        public TObject Prefab;

        private Vector3 _pos;
        private Transform _parent;

        public void CreatePool(int poolSize, Transform position, Transform parent)
        {
            _pos = position.position;
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
            if (_pool.Count > 0)
            {
                TObject obj = _pool.Dequeue();
                obj.gameObject.SetActive(true);
                return obj;
            }
            
            TObject newObj = Object.Instantiate(Prefab, _pos, Quaternion.identity, _parent);
            newObj.gameObject.SetActive(true);
            return newObj;
        }

        public void ReturnObject(TObject obj)
        {
            obj.gameObject.SetActive(false);
            _pool.Enqueue(obj);
        }
    }
}