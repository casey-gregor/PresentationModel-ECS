using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace ECSHomework
{
    public sealed class CheckIfCanMove : IEcsRunSystem
    { 
        private readonly EcsFilterInject<Inc<CanMove>> _filter;

        private readonly EcsPoolInject<MoveAllowed> _moveAllowedPool;
        
        public void Run(EcsSystems systems)
        {
            EcsPool<CanMove> canMovePool = _filter.Pools.Inc1;

            foreach (var entity in _filter.Value)
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