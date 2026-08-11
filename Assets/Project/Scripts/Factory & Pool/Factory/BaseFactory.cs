using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace Project.Scripts.Factory
{
    public abstract class BaseFactory<TObject> where TObject : Object
    {
        [Inject] private DiContainer _container;

        public TObject Create(TObject prefab)
        {
            return Instantiate(prefab);
        }
        
        public TObject Create(TObject prefab, Vector3 position)
        {
            return Instantiate(prefab, position);
        }
        
        public TObject Create(TObject prefab, Vector3 position, Quaternion rotation)
        {
            return Instantiate(prefab, position, rotation);
        }
        
        public TObject Create(TObject prefab, Vector3 position, Quaternion rotation, Transform parent)
        {
            return Instantiate(prefab, position, rotation, parent);
        }
        
        public TObject Create(TObject prefab, Vector3 position, Transform parent)
        {
            return Instantiate(prefab, position, Quaternion.identity, parent);
        }
        
        private TObject Instantiate(TObject prefab, Vector3? pos = null, Quaternion? rot = null, Transform parent = null)
        {
            Vector3 position = pos ?? Vector3.zero;
            Quaternion rotation = rot ?? Quaternion.identity;
            TObject entity = _container.InstantiatePrefabForComponent<TObject>(prefab, position, rotation, parent);
            return entity;
        }
    }
}
