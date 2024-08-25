using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace ECSHomework
{
    public class DeathEventSystem : IEcsRunSystem
    {
        private EcsFilterInject<Inc<DeathRequest, EntityObject>, Exc<Inactive>> _filter;
        
        private EcsPoolInject<Inactive> _inactivePool;
        
        public void Run(EcsSystems systems)
        {
            EcsPool<DeathRequest> deathRequestPool = _filter.Pools.Inc1;
            EcsPool<EntityObject> entityPool = _filter.Pools.Inc2;
            // Debug.Log("_filter : " + _filter.Value.GetEntitiesCount());
            
            foreach (int entity in _filter.Value)
            {
                // Debug.Log("deactivate entity : " + entity);
                deathRequestPool.Del(entity);
                
                _inactivePool.Value.Add(entity);
                entityPool.Get(entity).Value.gameObject.SetActive(false);
            }
        }
    }
}