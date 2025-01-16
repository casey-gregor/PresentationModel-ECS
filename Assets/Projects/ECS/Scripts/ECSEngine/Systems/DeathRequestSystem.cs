using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace ECSProject
{
    public sealed class DeathRequestSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<Health>, Exc<DeathRequest, AnimatorComponent, Inactive>> _filter;
        
        private readonly EcsPoolInject<DeathRequest> _deathRequestPool;
         
        public void Run(EcsSystems systems)
        {
            EcsPool<Health> healthPool = _filter.Pools.Inc1;

            foreach (int entity in _filter.Value)
            {
                int currentHealth = healthPool.Get(entity).CurrentValue;

                if (currentHealth <= 0)
                {
                    _deathRequestPool.Value.Add(entity) = new DeathRequest();
                }
            }
        }
    }
}