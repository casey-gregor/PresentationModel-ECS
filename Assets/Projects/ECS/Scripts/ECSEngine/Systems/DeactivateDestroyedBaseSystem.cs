using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace ECSProject
{
    public sealed class DeactivateDestroyedBaseSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<
            Inc<DeathRequest, EntityObject, IsBase, Timer, ColliderComponent, MeshRendererComponent>, 
            Exc<Inactive>> _filter;
        
        private readonly EcsPoolInject<RequesSetInactive> _requestSetInactivePool;
        private readonly EcsPoolInject<DelayedCheckComponent> _delayedCheckPool;
        
        public void Run(EcsSystems systems)
        {
            EcsPool<DeathRequest> deathRequestPool = _filter.Pools.Inc1;
            EcsPool<EntityObject> entityPool = _filter.Pools.Inc2;
            EcsPool<ColliderComponent> colliderPool = _filter.Pools.Inc5;
            EcsPool<MeshRendererComponent> meshRendererPool = _filter.Pools.Inc6;
            
            foreach (int entity in _filter.Value)
            {
                GameObject baseObject = entityPool.Get(entity).Value.gameObject;
                MeshRenderer meshRenderer = meshRendererPool.Get(entity).Value;
                Collider collider = colliderPool.Get(entity).Value;
                
                collider.gameObject.SetActive(false);
                meshRenderer.gameObject.SetActive(false);
                _requestSetInactivePool.Value.Add(entity);
                
                if (!_delayedCheckPool.Value.Has(entity))
                {
                    _delayedCheckPool.Value.Add(entity);
                }
                ref DelayedCheckComponent delayedCheck = ref _delayedCheckPool.Value.Get(entity);
                
                delayedCheck.Callback = () =>
                {
                    baseObject.SetActive(false);
                    deathRequestPool.Del(entity);
                    systems.GetWorld().DelEntity(entity);
                };
                
            }
        }
    }
}