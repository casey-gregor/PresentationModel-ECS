using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace ECSHomework
{
    public sealed class CheckIfCanAttackSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<CanAttack>> _filter;

        private readonly EcsPoolInject<AttackAllowed> _attackAllowedPool;
        
        public void Run(EcsSystems systems)
        {
            EcsPool<CanAttack> canAttackPool = _filter.Pools.Inc1;

            foreach (var entity in _filter.Value)
            {
                bool canAttack = canAttackPool.Get(entity).Value;
                bool hasAttackAllowed = _attackAllowedPool.Value.Has(entity);
                
                if (canAttack && !hasAttackAllowed)
                {
                    _attackAllowedPool.Value.Add(entity);
                }
                else if(!canAttack && hasAttackAllowed)
                {
                    _attackAllowedPool.Value.Del(entity);
                }
            }
        }
    }
}