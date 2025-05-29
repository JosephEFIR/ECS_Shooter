using Leopotam.Ecs;
using Project.Scripts.Move;
using UnityEngine;

namespace Project.Scripts.Animation
{
    sealed class PlayerAnimationSystem : IEcsRunSystem
    {
        private readonly EcsFilter<PlayerMovableComponent, PlayerAnimationComponent> _playerAnimationFilter = null;
        
        private float velocityZ;
        private float velocityX;
        private float walkAcceleration = 3f;
        private float runAcceleration = 5f;
        private float deceleration = 4f;
        private float maxWalkValue = 0.5f;
        private float maxRunValue = 2f;     
        //TODO Config?

        public void Run()
        {
            foreach (var i in _playerAnimationFilter)
            {
                ref var movableComponent = ref _playerAnimationFilter.Get1(i);
                ref var animComponent = ref _playerAnimationFilter.Get2(i);
                
                Move(animComponent.Animator, movableComponent);
            }
        }

        private void Move(Animator animator, PlayerMovableComponent movableComponent) //TODO REFACTORE CODE PLS OMG
        {
            ref var character = ref movableComponent.CharController;
            ref var isRun = ref movableComponent.IsRun;
            
            bool forward = Input.GetKey(KeyCode.W);
            bool backward = Input.GetKey(KeyCode.S);
            bool left = Input.GetKey(KeyCode.A);
            bool right = Input.GetKey(KeyCode.D);
            
            float currentAcceleration = isRun ? runAcceleration : walkAcceleration;
            float currentMaxValue = isRun ? maxRunValue : maxWalkValue;
            
            if (forward && velocityZ < currentMaxValue)
                velocityZ += Time.deltaTime * currentAcceleration;
            else if (backward && velocityZ > -currentMaxValue)
                velocityZ -= Time.deltaTime * currentAcceleration;
            else
            {
                velocityZ = Mathf.MoveTowards(velocityZ, 0, Time.deltaTime * deceleration);
            }
            
            if (left && velocityX > -currentMaxValue)
                velocityX -= Time.deltaTime * currentAcceleration;
            else if (right && velocityX < currentMaxValue)
                velocityX += Time.deltaTime * currentAcceleration;
            else
            {
                velocityX = Mathf.MoveTowards(velocityX, 0, Time.deltaTime * deceleration);
            }
            
            animator.SetFloat(EAnimParameter.VelocityX.ToString(), velocityX);
            animator.SetFloat(EAnimParameter.VelocityZ.ToString(), velocityZ);
            animator.SetFloat(EAnimParameter.Speed.ToString(), character.velocity.magnitude);
        }
    }
}