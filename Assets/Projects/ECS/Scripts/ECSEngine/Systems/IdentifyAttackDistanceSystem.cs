using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace ECSHomework
{
    public class IdentifyAttackDistanceSystem : IEcsRunSystem
    {
        private EcsFilterInject<Inc<MoveDirection, TargetEntity, Position, AttackDistance>, 
            Exc<Inactive, DeathRequest>> _filter;

        private EcsPoolInject<ReadyForAttack> readyForAttackPool;
        
        public void Run(EcsSystems systems)
        {
            var moveDirectionPool = _filter.Pools.Inc1;
            var targetPool = _filter.Pools.Inc2;
            var positionPool = _filter.Pools.Inc3;
            var attackDistancePool = _filter.Pools.Inc4;

            foreach (int entity in _filter.Value)
            {
                Entity targetEntity = targetPool.Get(entity).Value;
                
                if (targetEntity != null)
                {
                    Vector3 entityPosition = positionPool.Get(entity).Value;
                    Vector3 targetPosition = targetEntity.transform.position;
                    ref MoveDirection moveDirection = ref moveDirectionPool.Get(entity);
                    
                    float attackDistance = attackDistancePool.Get(entity).Value;
                    float distanceToTarget = (targetPosition - entityPosition).magnitude;

                    if (distanceToTarget <= attackDistance && !readyForAttackPool.Value.Has(entity))
                    {
                        Debug.Log("ready for attack");
                        readyForAttackPool.Value.Add(entity);
                    }
                }
            }
        }
    }
}