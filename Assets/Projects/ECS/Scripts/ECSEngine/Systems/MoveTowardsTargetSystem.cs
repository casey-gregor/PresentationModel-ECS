using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace ECSHomework
{
    public sealed class MoveTowardsTargetSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<MoveDirection, TargetEntity, Position, MoveSpeed>, 
                Exc<Inactive, DeathRequest>> _filter;
            
        public void Run(EcsSystems systems)
        {
            var moveDirectionPool = _filter.Pools.Inc1;
            var targetPool = _filter.Pools.Inc2;
            var positionPool = _filter.Pools.Inc3;

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