using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace ECSProject
{
    public sealed class RequestSetSoldierEntityInactive : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<Health>, Exc<Inactive, IsBase>> _filter;
        
        private readonly EcsPoolInject<RequesSetInactive> _requestSetInactivePool;
        
        public void Run(EcsSystems systems)
        {
            EcsPool<Health> healthPool = _filter.Pools.Inc1;
            foreach (var entity in _filter.Value)
            {
                int currentHealth = healthPool.Get(entity).CurrentValue;
                
                if (currentHealth <= 0)
                {
                    _requestSetInactivePool.Value.Add(entity);
                }
            }
        }
    }
}