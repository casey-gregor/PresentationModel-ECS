using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace ECSHomework
{
    public sealed class DeactivateDestroyedBaseSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<DeathRequest, EntityObject, IsBase>, Exc<Inactive>> _filter;
        private readonly EcsPoolInject<UnitTypeComponent> _unitTypesPool;
        
        
        public void Run(EcsSystems systems)
        {
            EcsPool<DeathRequest> deathRequestPool = _filter.Pools.Inc1;
            EcsPool<EntityObject> entityPool = _filter.Pools.Inc2;
            
            foreach (int entity in _filter.Value)
            {
                UnitTypes type = _unitTypesPool.Value.Get(entity).Value;
                GameObject baseObject = entityPool.Get(entity).Value.gameObject;
                
                GameObject meshObject = baseObject.transform.GetComponentInChildren<MeshRenderer>().gameObject;
                    
                meshObject.SetActive(false);
                
                deathRequestPool.Del(entity);
            }
        }
    }
}