using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Sirenix.Utilities;
using UnityEngine;

namespace ECSHomework
{
    public sealed class TransformViewSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<TransformView, PositionComponent>> _filter;
        
        private readonly EcsPoolInject<RotationComponent> _rotationPool;

        public void Run(EcsSystems systems)
        {
            EcsPool<RotationComponent> rotationPool = _rotationPool.Value;
            
            foreach (int entity in _filter.Value)
            {
                ref TransformView view = ref _filter.Pools.Inc1.Get(entity);
                
                PositionComponent positionComponent = _filter.Pools.Inc2.Get(entity);
             
                view.Value.position = positionComponent.Value;

                if (rotationPool.Has(entity))
                {
                    Quaternion rotation = rotationPool.Get(entity).Value;
                    view.Value.rotation = rotation;
                }
            }
        }
    }
    
}