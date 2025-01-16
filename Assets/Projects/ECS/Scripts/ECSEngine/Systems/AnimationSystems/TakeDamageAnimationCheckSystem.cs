using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace ECSProject
{
    public sealed class TakeDamageAnimationCheckSystem : IEcsRunSystem
    {
        private static readonly int TakeDamageAnimationParameter = Animator.StringToHash("Damaged");

        private readonly EcsFilterInject<Inc<IsDamaged>, Exc<Inactive>> _filter = EcsWorlds.EVENTS_WORLD;

        private readonly EcsPoolInject<AnimatorComponent> _animatorPool;

        public void Run(EcsSystems systems)
        {
            EcsPool<IsDamaged> isDamagedPool = _filter.Pools.Inc1;

            foreach (int entity in _filter.Value)
            {
                int damagedEntity = isDamagedPool.Get(entity).DamagedEntity;

                if (_animatorPool.Value.Has(damagedEntity))
                {
                    Animator animator = _animatorPool.Value.Get(damagedEntity).Value;
                    
                    animator.SetTrigger(TakeDamageAnimationParameter);
                }
            }
        }
    }
}