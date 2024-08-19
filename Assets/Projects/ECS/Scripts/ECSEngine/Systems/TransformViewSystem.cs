using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Sirenix.Utilities;
using UnityEngine;

namespace ECSHomework
{
    public sealed class TransformViewSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<TransformView, Position>> _filter;
        
        private readonly EcsPoolInject<Rotation> _rotationPool;

        public void Run(EcsSystems systems)
        {
            EcsPool<Rotation> rotationPool = _rotationPool.Value;
            
            foreach (int entity in _filter.Value)
            {
                ref TransformView view = ref _filter.Pools.Inc1.Get(entity);
                
                Position position = _filter.Pools.Inc2.Get(entity);
                
                // if (view.Value == null)
                // {
                //     Debug.LogError($"Entity {entity} has a null TransformView.Value!");
                //     continue;
                // }
             
                view.Value.position = position.Value;

                if (rotationPool.Has(entity))
                {
                    Quaternion rotation = rotationPool.Get(entity).Value;
                    view.Value.rotation = rotation;
                }
            }
        }
    }
    
}