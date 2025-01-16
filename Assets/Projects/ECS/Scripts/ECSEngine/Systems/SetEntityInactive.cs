using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace ECSProject
{
    public sealed class SetEntityInactive : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<RequesSetInactive>> _filter;
        
        private readonly EcsPoolInject<Inactive> _inactivePool;
        
        public void Run(EcsSystems systems)
        {
            EcsPool<RequesSetInactive> requestSetInactivePool = _filter.Pools.Inc1;

            foreach (var entity in _filter.Value)
            {
                _inactivePool.Value.Add(entity);
                requestSetInactivePool.Del(entity);
            }
        }
    }
}