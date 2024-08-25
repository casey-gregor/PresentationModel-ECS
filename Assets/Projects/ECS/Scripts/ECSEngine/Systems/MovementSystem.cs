using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace ECSHomework.Systems
{
    public sealed class MovementSystem : IEcsRunSystem
    {
        private EcsFilterInject<Inc<MoveDirection, MoveSpeed, Position>, Exc<ReadyForAttack>> _filter;
        
        public void Run(EcsSystems systems)
        {
            float deltaTime = Time.deltaTime;
        
            EcsPool<MoveDirection> directionPool = _filter.Pools.Inc1;
            EcsPool<MoveSpeed> speedPool = _filter.Pools.Inc2;
            EcsPool<Position> positionPool = _filter.Pools.Inc3;
            
            foreach (var entity in _filter.Value)
            {
                MoveDirection moveDirection = directionPool.Get(entity);
                MoveSpeed moveSpeed = speedPool.Get(entity);
                ref Position position = ref positionPool.Get(entity);
                position.Value += moveDirection.Value *(moveSpeed.Value * deltaTime);
            }
        }
    }
}