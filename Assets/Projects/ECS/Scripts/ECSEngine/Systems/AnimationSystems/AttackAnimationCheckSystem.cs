using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace ECSProject
{
    public sealed class AttackAnimationCheckSystem : IEcsRunSystem
    {
        private static readonly int AttackAnimationParameter = Animator.StringToHash("Attack");

        private readonly EcsFilterInject<
            Inc<TriggerAttack>,
            Exc<GameOver, Inactive>> _filter = EcsWorlds.EVENTS_WORLD;
        
        private readonly EcsPoolInject<IsAttacking> _isAttackingPool;

        private readonly EcsPoolInject<AnimatorComponent> _animatorPool;
        
        public void Run(EcsSystems systems)
        {
            EcsPool<TriggerAttack> triggerAttackPool = _filter.Pools.Inc1;
            
            foreach (int entity in _filter.Value)
            {
                int attackingEntity = triggerAttackPool.Get(entity).Entity;
                
                Animator animator = _animatorPool.Value.Get(attackingEntity).Value;
                
                animator.SetTrigger(AttackAnimationParameter);

                if (!_isAttackingPool.Value.Has(attackingEntity))
                {
                    _isAttackingPool.Value.Add(attackingEntity);
                }
            }
        }
    }
}