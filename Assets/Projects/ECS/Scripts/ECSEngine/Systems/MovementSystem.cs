using Leopotam.EcsLite;
using UnityEngine;

namespace ECSHomework.Systems
{
    public sealed class MovementSystem : IEcsRunSystem
    {
        public void Run(EcsSystems systems)
        {
            float deltaTime = Time.deltaTime;
            EcsWorld ecsWorld = systems.GetWorld();

            EcsFilter filter = ecsWorld.Filter<MoveDirection>().Inc<MoveSpeed>().Inc<Position>().End();
            
            EcsPool<MoveDirection> directionPool = ecsWorld.GetPool<MoveDirection>();
            EcsPool<MoveSpeed> speedPool = ecsWorld.GetPool<MoveSpeed>();
            EcsPool<Position> positionPool = ecsWorld.GetPool<Position>();
            
            foreach (var entity in filter)
            {
                MoveDirection moveDirection = directionPool.Get(entity);
                MoveSpeed moveSpeed = speedPool.Get(entity);
                ref Position position = ref positionPool.Get(entity);
                
                position.Value += moveDirection.Value *(moveSpeed.Value * deltaTime);
            }
        }
    }
}