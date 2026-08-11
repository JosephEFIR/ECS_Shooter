using Leopotam.Ecs;
using Project.Scripts.Animation;
using UnityEngine;

namespace Project.Scripts.Move
{
    public class PlayerCrouchSystem : IEcsRunSystem
    {
        private readonly EcsFilter<PlayerMovableComponent, PlayerAnimationComponent> _filter = null;
        
        private readonly Vector3 _crouchScale = new (1, 0.5f, 1); //забей так надо
        private readonly Vector3 _defaultScale = new (1, 1f, 1);
        
        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var movableComponent = ref _filter.Get1(i);
                ref var animationComponent = ref _filter.Get2(i);

                Transform movableCollider = movableComponent.Collider.transform;
                movableComponent.IsCrouched = movableComponent.CrouchChecker.IsCrouch;
                
                movableComponent.IsCrouched = Input.GetKey(KeyCode.LeftControl);
                
                if (Input.GetKeyDown(KeyCode.LeftControl))
                {
                    movableCollider.transform.localScale = _crouchScale;
                    movableCollider.transform.position 
                        = new Vector3(movableCollider.position.x, movableCollider.position.y - 0.5f, movableCollider.position.z);
                }
                else if(Input.GetKeyUp(KeyCode.LeftControl))
                {
                    movableCollider.transform.localScale = _defaultScale;
                    movableCollider.transform.position 
                        = new Vector3(movableCollider.position.x, movableCollider.position.y + 0.5f, movableCollider.position.z);
                }
                
                animationComponent.Animator.SetBool(EAnimParameter.IsCrouched.ToString(), movableComponent.IsCrouched);
            }
        }
    }
}