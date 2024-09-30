using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace ECSHomework
{
    public sealed class AttackAnimationCheckSystem : IEcsRunSystem
    {
        private static readonly int AttackingAnimationParameter = Animator.StringToHash("Attack");

        private readonly EcsFilterInject<
            Inc<IsAttacking>,
            Exc<GameOver, Inactive>> _filter = EcsWorlds.EVENTS_WORLD;

        private readonly EcsPoolInject<AnimatorComponent> _animatorPool;
        
        public void Run(EcsSystems systems)
        {
            EcsPool<IsAttacking> isAttackingPool = _filter.Pools.Inc1;
            
            foreach (int entity in _filter.Value)
            {
                int attackingEntity = isAttackingPool.Get(entity).Entity;
                
                Animator animator = _animatorPool.Value.Get(attackingEntity).Value;
                
                animator.SetTrigger(AttackingAnimationParameter);
                
            }
        }
    }
}