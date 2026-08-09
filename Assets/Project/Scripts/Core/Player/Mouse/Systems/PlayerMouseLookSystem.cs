using Leopotam.Ecs;
using Project.Scripts.Common;
using Project.Scripts.Tags;
using Project.Scripts.Weapon;
using UnityEngine;

namespace Project.Scripts.Move
{
    sealed class PlayerMouseLookSystem :  IEcsRunSystem //TODO ALARM FIX!!!!!!!
    {
        private readonly EcsFilter<PlayerComponent, ModelComponent, MouseLookComponent,CamerasComponent, PlayerMovableComponent> _mouseLookFilter = null;
        
        public void Run()
        {
            foreach (var i in _mouseLookFilter)
            {
                ref var entity = ref _mouseLookFilter.GetEntity(i);
                ref var model = ref _mouseLookFilter.Get2(i);
                ref var mouseLookComponent = ref _mouseLookFilter.Get3(i);
                ref var cameraSwitcherComponent = ref _mouseLookFilter.Get4(i);
                ref var movableComponent = ref _mouseLookFilter.Get5(i);
                
                ref var isFpv = ref cameraSwitcherComponent.isFPV;
                ref var axisX = ref mouseLookComponent.Direction.x;
                ref var axisY = ref mouseLookComponent.Direction.y;
                if (isFpv)
                {
                    entity.Get<AimComponent>();
                    model.ModelTransform.rotation = 
                          model.StartRotation * Quaternion.AngleAxis(axisX, Vector3.up * Time.deltaTime * mouseLookComponent.Sensitivity);
                   //mouseLookComponent.Camera.transform.rotation =
                     //   model.ModelTransform.rotation * Quaternion.AngleAxis(axisY, Vector3.right * Time.deltaTime * mouseLookComponent.Sensitivity);
                }
                else
                {
                    entity.Del<AimComponent>();
                    if (movableComponent.Rigidbody.linearVelocity.sqrMagnitude > 0.01f)
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
    }
}