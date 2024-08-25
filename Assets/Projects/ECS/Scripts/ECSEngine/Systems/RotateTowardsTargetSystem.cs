using ECSHomework;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace ECSHomework
{
    public class RotateTowardsTargetSystem : IEcsRunSystem
    {
        private EcsFilterInject<Inc<Position, TargetEntity, Rotation>> _filter;


        public void Run(EcsSystems systems)
        {
            EcsPool<Position> positionPool = _filter.Pools.Inc1;
            EcsPool<TargetEntity> targetEntityPool = _filter.Pools.Inc2;
            EcsPool<Rotation> rotationPool = _filter.Pools.Inc3;

            foreach (var entity in _filter.Value)
            {
                if (targetEntityPool.Get(entity).Value != null)
                {
                    Vector3 entityPosition = positionPool.Get(entity).Value;
                    Vector3 targetPosition = targetEntityPool.Get(entity).Value.transform.position;
                    Vector3 directionToTarget = targetPosition - entityPosition;
                    
                    Quaternion targetRotation = Quaternion.LookRotation(directionToTarget, Vector3.up);
                    Quaternion entityRotation = rotationPool.Get(entity).Value;
                    
                    rotationPool.Get(entity).Value = Quaternion.Lerp(entityRotation, targetRotation, 0.1f);
                    
                }
            }
        }
    }
}