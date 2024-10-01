using ECSHomework;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace ECSHomework
{
    public class ExampleSystem : IEcsInitSystem
    {
        private readonly EcsPoolInject<PositionComponent> _positionPool;
        private readonly EcsPoolInject<MoveDirection> _moveDirectionPool;
        private readonly EcsPoolInject<MoveSpeed> _moveSpeedPool;

        
        public void Init(EcsSystems systems)
        {
            int newEntity = systems.GetWorld().NewEntity();
            
            _positionPool.Value.Add(newEntity) = new PositionComponent{ Value = Vector3.zero };
            _moveDirectionPool.Value.Add(newEntity) = new MoveDirection { Value = Vector3.up };
            _moveSpeedPool.Value.Add(newEntity) = new MoveSpeed { Value = 5 };
            
        }
    }
}