using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace ECSProject
{
    public sealed class RotateTowardsTargetSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<MoveAllowed, PositionComponent, TargetEntity, RotationComponent>,
        Exc<IsAttacking>> _filter;
        private const float RotationSpeed = 0.1f;

        public void Run(EcsSystems systems)
        {
            EcsPool<PositionComponent> positionPool = _filter.Pools.Inc2;
            EcsPool<TargetEntity> targetEntityPool = _filter.Pools.Inc3;
            EcsPool<RotationComponent> rotationPool = _filter.Pools.Inc4;

            foreach (var entity in _filter.Value)
            {
                Entity targetEntity = targetEntityPool.Get(entity).Value;
                
                if (targetEntity != null)
                {
                    Vector3 entityPosition = positionPool.Get(entity).Value;
                    Vector3 targetPosition = targetEntityPool.Get(entity).Value.transform.position;
                    Vector3 directionToTarget = targetPosition - entityPosition;
                    
                    Quaternion targetRotation = Quaternion.LookRotation(directionToTarget, Vector3.up);
                    Quaternion entityRotation = rotationPool.Get(entity).Value;
                    
                    rotationPool.Get(entity).Value = Quaternion.Lerp(entityRotation, targetRotation, RotationSpeed);
                }
            }
        }
    }
}