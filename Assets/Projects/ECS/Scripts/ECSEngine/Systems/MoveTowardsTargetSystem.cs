using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace ECSHomework
{
    public class MoveTowardsTargetSystem : IEcsRunSystem
    {
        private EcsFilterInject<Inc<MoveDirection, TargetEntity, Position>, 
                Exc<Inactive, DeathRequest>> _filter;
        
        private EcsPoolInject<Rotation> _rotationPool;
            
        public void Run(EcsSystems systems)
        {
            var moveDirectionPool = _filter.Pools.Inc1;
            var targetPool = _filter.Pools.Inc2;
            var positionPool = _filter.Pools.Inc3;

            var rotationPool = _rotationPool.Value;

            foreach (var entity in _filter.Value)
            {
                Entity targetEntity = targetPool.Get(entity).Value;
                if (targetEntity != null)
                {
                    Vector3 entityTransform = positionPool.Get(entity).Value;
                    Vector3 directionToTarget = targetEntity.transform.position - entityTransform;
                    
                    moveDirectionPool.Get(entity).Value = directionToTarget.normalized;
                }
            }
        }
    }
}