using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace ECSHomework
{
    public sealed class DeactivateDestroyedBaseSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<DeathRequest, EntityObject, IsBase>, Exc<Inactive>> _filter;

        private readonly EcsWorldInject _eventsWorld = EcsWorlds.EVENTS_WORLD;

        private readonly EcsPoolInject<OneFrame> _oneFramePool = EcsWorlds.EVENTS_WORLD;
        private readonly EcsPoolInject<DeathRequest> _deathRequestPoolEventWorld = EcsWorlds.EVENTS_WORLD;
        
        public void Run(EcsSystems systems)
        {
            EcsPool<DeathRequest> deathRequestPool = _filter.Pools.Inc1;
            EcsPool<EntityObject> entityPool = _filter.Pools.Inc2;
            
            foreach (int entity in _filter.Value)
            {
                GameObject baseObject = entityPool.Get(entity).Value.gameObject;
                
                GameObject meshObject = baseObject.transform.GetComponentInChildren<MeshRenderer>().gameObject;
                    
                meshObject.SetActive(false);
                
                deathRequestPool.Del(entity);
            }
        }
    }
}