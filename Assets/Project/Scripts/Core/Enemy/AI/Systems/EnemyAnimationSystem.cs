using Leopotam.Ecs;
using Project.Scripts.Animation;

namespace Project.Scripts.Core.Enemy.AI.Systems
{
    public class EnemyAnimationSystem : IEcsRunSystem
    {
        private readonly EcsFilter<EnemyUnitComponent, AnimationComponent> _filter = null;
        
        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var enemyComp = ref _filter.Get1(i);
                ref var animationComp= ref _filter.Get2(i);
                
                animationComp.Animator.SetFloat(EAnimParameter.VelocityX.ToString(), enemyComp.Agent.velocity.x);
                animationComp.Animator.SetFloat(EAnimParameter.VelocityZ.ToString(),enemyComp.Agent.velocity.z);
            }
        }
    }
}