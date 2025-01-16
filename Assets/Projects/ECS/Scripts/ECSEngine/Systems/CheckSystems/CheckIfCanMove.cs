using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace ECSProject
{
    public sealed class CheckIfCanMove : IEcsRunSystem
    { 
        private readonly EcsFilterInject<Inc<CanMove>> _canMoveFilter;//Is a parameter set in installer on start

        private readonly EcsPoolInject<MoveAllowed> _moveAllowedPool;//Is a parameter used in Systems to check
        
        public void Run(EcsSystems systems)
        {
            EcsPool<CanMove> canMovePool = _canMoveFilter.Pools.Inc1;

            foreach (var entity in _canMoveFilter.Value)
            {
                bool canMove = canMovePool.Get(entity).Value;
                bool hasMoveAllowed = _moveAllowedPool.Value.Has(entity);
                
                if (canMove && !hasMoveAllowed)
                {
                    _moveAllowedPool.Value.Add(entity);
                }
                else if(!canMove && hasMoveAllowed)
                {
                    _moveAllowedPool.Value.Del(entity);
                }
            }
        }
    }
}