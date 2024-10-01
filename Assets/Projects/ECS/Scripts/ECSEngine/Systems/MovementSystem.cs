using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace ECSHomework.Systems
{
    public sealed class MovementSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<
            Inc<MoveAllowed, MoveDirection, MoveSpeed, PositionComponent>, 
            Exc<ReadyForAttack, GameOver, Inactive>> _filter;
        
        public void Run(EcsSystems systems)
        {
            float deltaTime = Time.deltaTime;
            
            EcsPool<MoveDirection> directionPool = _filter.Pools.Inc2;
            EcsPool<MoveSpeed> speedPool = _filter.Pools.Inc3;
            EcsPool<PositionComponent> positionPool = _filter.Pools.Inc4;
            
            foreach (var entity in _filter.Value)
            {
                MoveDirection moveDirection = directionPool.Get(entity);
                MoveSpeed moveSpeed = speedPool.Get(entity);
                ref PositionComponent positionComponent = ref positionPool.Get(entity);
                
                positionComponent.Value += moveDirection.Value *(moveSpeed.Value * deltaTime);
            }
        }
    }
}