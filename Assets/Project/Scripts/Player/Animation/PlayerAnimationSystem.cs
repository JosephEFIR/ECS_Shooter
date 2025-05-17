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
            animator.SetFloat("Speed", characterController.velocity.magnitude);
        }

        private void Run(Animator animator, bool isRun)
        {
            animator.SetBool("isRun", isRun);
        }
        
    }
}

