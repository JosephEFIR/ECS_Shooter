using Leopotam.Ecs;
using Project.Scripts.Animation;
using Project.Scripts.Common;
using Project.Scripts.Tags;
using UnityEngine;

namespace Project.Scripts.Move
{
    sealed class PlayerMouseLookSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly EcsFilter<PlayerComponent> _playerFilter = null;
        private readonly EcsFilter<PlayerComponent, ModelComponent, MouseLookComponent,CameraSwitcherComponent, PlayerMovableComponent> _mouseLookFilter = null;

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
                ref var cameraSwitcherComponent = ref _mouseLookFilter.Get4(i);
                ref var movableComponent = ref _mouseLookFilter.Get5(i);
                if(cameraSwitcherComponent.isFPV) FirstPersonView(model, mouseLookComponent);
                else ThirdPersonView(model, mouseLookComponent, movableComponent);
            }
            
        }

        private void FirstPersonView(ModelComponent model, MouseLookComponent mouseLookComponent)
        {
            var axisX = mouseLookComponent.Direction.x;
            var axisY = mouseLookComponent.Direction.y;
                
            var rotateX = 
                Quaternion.AngleAxis(axisX, Vector3.up * Time.deltaTime * mouseLookComponent.Sensitivity);
            var rotateY = 
                Quaternion.AngleAxis(axisY, Vector3.right * Time.deltaTime * mouseLookComponent.Sensitivity);
                
            model.ModelTransform.rotation = _startTransformRotation * rotateX;
            mouseLookComponent.Camera.transform.rotation = model.ModelTransform.rotation * rotateY;
        }

        private void ThirdPersonView(ModelComponent model, MouseLookComponent mouseLookComponent, PlayerMovableComponent movable)
        {
            if (movable.Rigidbody.linearVelocity.sqrMagnitude > 0.01f)
            {
                Vector3 cameraForward = mouseLookComponent.Camera.transform.forward;
                cameraForward.y = 0f;
                cameraForward.Normalize();
            
                Quaternion targetRotation = Quaternion.LookRotation(cameraForward);
                model.ModelTransform.rotation = Quaternion.RotateTowards(
                    model.ModelTransform.rotation,
                    targetRotation,
                    Time.deltaTime * 350F);//TODO A
            }
        }
    }
}