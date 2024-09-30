using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace ECSHomework
{
    public class RequestSetEntityInactive : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<Health>, Exc<Inactive>> _filter;
        
        private readonly EcsPoolInject<RequesSetInactive> _requesSetInactivePool;
        
        public void Run(EcsSystems systems)
        {
            EcsPool<Health> healthPool = _filter.Pools.Inc1;
            foreach (var entity in _filter.Value)
            {
                int currentHealth = healthPool.Get(entity).CurrentValue;
                
                if (currentHealth <= 0)
                {
                    _requesSetInactivePool.Value.Add(entity);
                }
            }
        }
    }
}