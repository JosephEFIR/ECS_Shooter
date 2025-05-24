using Leopotam.Ecs;
using Project.Scripts.Move;
using Project.Scripts.Tags;
using UnityEngine;

namespace Project.Scripts.Animation
{
    sealed class PlayerAnimationSystem : IEcsRunSystem
    {
        private readonly EcsFilter<PlayerMovableComponent,PlayerAnimationComponent> _playerAnimationFilter = null;
        
        public void Run()
        {
            foreach (var i in _playerAnimationFilter)
            {
                ref var movableComponent = ref _playerAnimationFilter.Get1(i);
                ref var animComponent = ref _playerAnimationFilter.Get2(i);
                
                Move(animComponent.Animator, movableComponent.CharController);
                Run(animComponent.Animator, movableComponent.IsRun);
            }
        }

        private void Move(Animator animator, CharacterController characterController)
        {
            //Extenstion не судьба написать для аниматора? что бы не писать постоянно ToString?
            animator.SetFloat(EAnimParameter.Speed.ToString(), characterController.velocity.magnitude);
            
            if (Input.GetKey(KeyCode.W)) animator.SetTrigger(EAnimParameter.isForward.ToString());
            if (Input.GetKey(KeyCode.S)) animator.SetTrigger(EAnimParameter.isBackward.ToString());
            if (Input.GetKey(KeyCode.A)) animator.SetTrigger(EAnimParameter.isLeft.ToString());
            if (Input.GetKey(KeyCode.D)) animator.SetTrigger(EAnimParameter.isRight.ToString());
        }

        private void Run(Animator animator, bool isRun)
        {
            animator.SetBool(EAnimParameter.isRun.ToString(), isRun);
        }
        
    }
}

