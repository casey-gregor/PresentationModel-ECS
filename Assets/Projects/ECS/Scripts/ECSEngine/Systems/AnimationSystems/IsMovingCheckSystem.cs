using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Unity.VisualScripting;
using UnityEngine;

namespace ECSHomework
{
    public sealed class IsMovingCheckSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<
            Inc<MoveAllowed, PositionComponent, PreviousPosition, AnimatorComponent>, 
            Exc<GameOver, Inactive, FireRequest, IsMoving>> _filter;
        
        private readonly EcsPoolInject<IsMoving> _isMovingPool;
        
        public void Run(EcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                PositionComponent currentPositionComponent = _filter.Pools.Inc2.Get(entity);
                ref PreviousPosition previousPosition = ref _filter.Pools.Inc3.Get(entity);

                if (currentPositionComponent.Value != previousPosition.Value)
                {
                    previousPosition.Value = currentPositionComponent.Value;
                    
                    _isMovingPool.Value.Add(entity);
                }
            }
        }
    }
}