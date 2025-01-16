using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace ECSProject
{
    public sealed class TrajectoryCalculationSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<TargetEntity, TrajectoryCalculateRequest>> _filter;
        
        private readonly EcsPoolInject<RigidbodyComponent> _rigidbodyPool;

        private const float MinHeight = 2f;
        private const float MaxHeight = 10f;
        private const float MaxDistance = 60f;
        
        public void Run(EcsSystems systems)
        {
            EcsPool<TargetEntity> targetEntityPool = _filter.Pools.Inc1;
            EcsPool<TrajectoryCalculateRequest> trajectoryCalculateRequestPool = _filter.Pools.Inc2;

            foreach (var entity in _filter.Value)
            {
                ref RigidbodyComponent rbComponent = ref _rigidbodyPool.Value.Get(entity);
                TargetEntity targetEntity = targetEntityPool.Get(entity);
                
                if(targetEntity.Value == null)
                    return;
                
                Vector3 targetPosition = targetEntityPool.Get(entity).Value.transform.position;
                Vector3 shootPosition = rbComponent.Value.transform.position;
                
                float distanceToTarget = (targetPosition - shootPosition).magnitude;
                float height = Mathf.Lerp(MinHeight, MaxHeight, distanceToTarget / MaxDistance);
                
                Vector3 initialVelocity = CalculateTrajectory(shootPosition, targetPosition, height);
                
                rbComponent.Value.linearVelocity = initialVelocity;

                trajectoryCalculateRequestPool.Del(entity);
            }
        }
        
        private Vector3 CalculateTrajectory(Vector3 shootPosition, Vector3 targetPosition, float maxHeight)
        {
            Vector3 displacement = targetPosition - shootPosition;
            Vector3 displacementXZ = new Vector3(displacement.x, 0, displacement.z);

            float time = Mathf.Sqrt(-2 * maxHeight / Physics.gravity.y) + Mathf.Sqrt(2 * (displacement.y - maxHeight) / Physics.gravity.y);

            Vector3 velocityXZ = displacementXZ / time;
            float velocityY = Mathf.Sqrt(-2 * Physics.gravity.y * maxHeight);

            return velocityXZ + velocityY * Vector3.up;
        }
    }
}