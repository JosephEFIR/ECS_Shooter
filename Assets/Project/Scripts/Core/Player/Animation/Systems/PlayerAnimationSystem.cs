using Leopotam.Ecs;
using Project.Scripts.Move;
using Project.Scripts.Tags;
using Project.Scripts.Weapon;
using UnityEngine;

namespace Project.Scripts.Animation
{
    sealed class PlayerAnimationSystem : IEcsRunSystem //TODO REFACTORE + REF
    {
        private readonly EcsFilter<PlayerComponent,PlayerMovableComponent, PlayerAnimationComponent> _playerAnimationFilter = null;
        
        //CONFIG?
        private float _velocityX;
        private float _velocityZ;
        private float _currentVelocityY;
        private float _smoothTime = 0.2f; 
        private Vector3 _smoothDampRef; 
        
        private float _walkAcceleration = 5f;
        private float _runAcceleration = 10f;
        private float _deceleration = 4f;
        private float _maxWalkValue = 2f;
        private float _maxRunValue = 4f;

        public void Run()
        {
            foreach (var i in _playerAnimationFilter)
            {
                ref var entity = ref _playerAnimationFilter.GetEntity(i);
                ref var playerComponent = ref _playerAnimationFilter.Get1(i);
                ref var movableComponent = ref _playerAnimationFilter.Get2(i);
                ref var animComponent = ref _playerAnimationFilter.Get3(i);

                Animator animator = animComponent.Animator;
                
                animator.SetFloat(EAnimParameter.VelocityX.ToString(), _velocityX);
                animator.SetFloat(EAnimParameter.VelocityZ.ToString(), _velocityZ);
                animator.SetFloat(EAnimParameter.Speed.ToString(), movableComponent.Rigidbody.linearVelocity.magnitude);
                animator.SetBool(EAnimParameter.IsGrounded.ToString(), movableComponent.IsGrounded);
             
                ref var animIK = ref playerComponent.AnimIK;
                if (entity.Has<AimComponent>())
                {
                    animIK.Rig.weight = 1;
                }
                else animIK.Rig.weight = 0;
                
                MoveSides(movableComponent);
                Jump(animComponent.Animator);
                Landing(animComponent.Animator, movableComponent);
            }
        }

        private void MoveSides(PlayerMovableComponent movableComponent)
        {
            float targetVelocityX = 0f;
            float targetVelocityZ = 0f;
            
            if (Input.GetKey(KeyCode.W)) targetVelocityZ = movableComponent.IsRun ? _maxRunValue : _maxWalkValue;
            if (Input.GetKey(KeyCode.S)) targetVelocityZ = movableComponent.IsRun ? -_maxRunValue : -_maxWalkValue;
            if (Input.GetKey(KeyCode.A)) targetVelocityX = movableComponent.IsRun ? -_maxRunValue : -_maxWalkValue;
            if (Input.GetKey(KeyCode.D)) targetVelocityX = movableComponent.IsRun ? _maxRunValue : _maxWalkValue;
            
            _velocityX = Mathf.SmoothDamp(_velocityX, targetVelocityX, ref _smoothDampRef.x, _smoothTime);
            _velocityZ = Mathf.SmoothDamp(_velocityZ, targetVelocityZ, ref _smoothDampRef.z, _smoothTime);

            if (Mathf.Abs(targetVelocityX) < 0.1f)
            {
                _velocityX = Mathf.MoveTowards(_velocityX, 0, _deceleration * Time.deltaTime);
            }

            if (Mathf.Abs(targetVelocityZ) < 0.1f)
            {
                _velocityZ = Mathf.MoveTowards(_velocityZ, 0, _deceleration * Time.deltaTime);
            }
        }

        private void Jump(Animator animator)
        {
            if (Input.GetKeyDown(KeyCode.Space)) animator.SetTrigger(EAnimParameter.JumpTrigger.ToString());
        }

        private void Landing(Animator animator, PlayerMovableComponent movableComponent)
        {
            _currentVelocityY = movableComponent.Rigidbody.linearVelocity.y;
            animator.SetFloat(EAnimParameter.VelocityY.ToString(), _currentVelocityY);

            if (!movableComponent.IsGrounded) animator.ResetTrigger(EAnimParameter.JumpTrigger.ToString());
        }
    }
}