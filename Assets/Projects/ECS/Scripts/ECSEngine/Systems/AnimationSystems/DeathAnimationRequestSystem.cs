using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace ECSHomework
{
    public sealed class DeathAnimationRequestSystem : IEcsRunSystem  
    {
        private readonly EcsFilterInject<Inc<Health, AnimatorComponent>, Exc<IsDeadAnimation, Inactive>> _filter;
        
        private readonly EcsPoolInject<IsDeadAnimation> _isDeadAnimationPool;
        private readonly EcsPoolInject<Inactive> _inactivePool;
         
        public void Run(EcsSystems systems)
        {
            EcsPool<Health> healthPool = _filter.Pools.Inc1;

            foreach (int entity in _filter.Value)
            {
                int health = healthPool.Get(entity).CurrentValue;

                if (health <= 0)
                {
                    _isDeadAnimationPool.Value.Add(entity);
                }
            }
        }
    }
}