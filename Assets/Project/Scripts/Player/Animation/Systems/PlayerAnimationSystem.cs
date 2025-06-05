using Leopotam.Ecs;
using Project.Scripts.Move;
using Unity.VisualScripting;
using UnityEngine;

namespace Project.Scripts.Animation
{
    sealed class PlayerAnimationSystem : IEcsRunSystem
    {
        private readonly EcsFilter<PlayerMovableComponent, PlayerAnimationComponent> _playerAnimationFilter = null;
        
        private float velocityX;
        private float velocityY;
        private float velocityZ;
        private float walkAcceleration = 3f;
        private float runAcceleration = 5f;
        private float deceleration = 4f;
        private float maxWalkValue = 1f;
        private float maxRunValue = 2f;     
        //TODO Config?

        public void Run()
        {
            foreach (var i in _playerAnimationFilter)
            {
                ref var movableComponent = ref _playerAnimationFilter.Get1(i);
                ref var animComponent = ref _playerAnimationFilter.Get2(i);
                
                animComponent.Animator.SetFloat(EAnimParameter.VelocityX.ToString(), velocityX);
                animComponent.Animator.SetFloat(EAnimParameter.VelocityY.ToString(), velocityY);
                animComponent.Animator.SetFloat(EAnimParameter.VelocityZ.ToString(), velocityZ);
                animComponent.Animator.SetFloat(EAnimParameter.Speed.ToString(), movableComponent.Rigidbody.linearVelocity.magnitude);
                animComponent.Animator.SetBool(EAnimParameter.IsGrounded.ToString(), movableComponent.IsGrounded);
                
                MoveSides(movableComponent);
                Jump(animComponent.Animator);
                Landing(animComponent.Animator, movableComponent);
            }
        }

        private void Jump(Animator animator)
        {
            bool jump = Input.GetKeyDown(KeyCode.Space);

            if (jump) animator.SetTrigger(EAnimParameter.JumpTrigger.ToString());
        }

        private void Landing(Animator animator, PlayerMovableComponent movableComponent)
        {
            if (!movableComponent.IsGrounded)
            {
                animator.ResetTrigger(EAnimParameter.JumpTrigger.ToString());
                animator.SetFloat(EAnimParameter.VelocityY.ToString(), velocityY);
            }
            velocityY = movableComponent.Rigidbody.linearVelocity.y;
        }

        private void MoveSides(PlayerMovableComponent movableComponent) 
        {
            ref var isRun = ref movableComponent.IsRun;
            
            bool forward = Input.GetKey(KeyCode.W);
            bool backward = Input.GetKey(KeyCode.S);
            bool left = Input.GetKey(KeyCode.A);
            bool right = Input.GetKey(KeyCode.D);
            
            float currentAcceleration = isRun ? runAcceleration : walkAcceleration;
            float currentMaxValue = isRun ? maxRunValue : maxWalkValue;

            if (forward && velocityZ < currentMaxValue)
            {
                velocityZ += Time.deltaTime * currentAcceleration;
            }
            else if (backward && velocityZ > -currentMaxValue)
            {
                velocityZ -= Time.deltaTime * currentAcceleration;
            }
            else
            {
                velocityZ = Mathf.MoveTowards(velocityZ, 0, Time.deltaTime * deceleration);
            }

            if (left && velocityX > -currentMaxValue)
            {
                velocityX -= Time.deltaTime * currentAcceleration;
            }
            else if (right && velocityX < currentMaxValue)
            {
                velocityX += Time.deltaTime * currentAcceleration;
            }
            else
            {
                velocityX = Mathf.MoveTowards(velocityX, 0, Time.deltaTime * deceleration);
            }
        }
    }
}