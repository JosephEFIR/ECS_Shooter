using Leopotam.Ecs;
using Project.Scripts.Common;
using Project.Scripts.Tags;
using UnityEngine;

namespace Project.Scripts.Move
{
    sealed class PlayerMouseLookSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly EcsFilter<PlayerTag> _playerFilter = null;
        private readonly EcsFilter<PlayerTag, ModelComponent, MouseLookComponent> _mouseLookFilter = null;

        private Quaternion _startTransformRotation;

        public void Init()
        {
            foreach (var i in _playerFilter)
            {
                ref var entity = ref _playerFilter.GetEntity(i);
                ref var model = ref entity.Get<ModelComponent>();

                _startTransformRotation = model.ModelTransform.rotation;
            }
        }
        
        public void Run()
        {
            foreach (var i in _mouseLookFilter)
            {
                ref var model = ref _mouseLookFilter.Get2(i);
                ref var mouseLookComponent = ref _mouseLookFilter.Get3(i);

                var axisX = mouseLookComponent.Direction.x;
                var axisY = mouseLookComponent.Direction.y;
                
                var rotateX = 
                    Quaternion.AngleAxis(axisX, Vector3.up * Time.deltaTime * mouseLookComponent.Sensitivity);
                var rotateY = 
                    Quaternion.AngleAxis(axisY, Vector3.right * Time.deltaTime * mouseLookComponent.Sensitivity);
                
                model.ModelTransform.rotation = _startTransformRotation * rotateX;
                mouseLookComponent.Camera.transform.rotation = model.ModelTransform.rotation * rotateY;
            }
        }
    }
}