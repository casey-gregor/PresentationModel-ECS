using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace ECSProject
{
    public sealed class DealDamageSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<
            Inc<TargetEntity, AttackDamageValue, DealDamageRequest>, Exc<Inactive>> _filter;

        private readonly EcsPoolInject<GetDamageValue> _getDamageValuePool;
        
        private readonly EcsWorldInject _eventWorld = EcsWorlds.EVENTS_WORLD;
        private readonly EcsPoolInject<IsDamaged> _isDamagedPool = EcsWorlds.EVENTS_WORLD;
        private readonly EcsPoolInject<OneFrame> _oneFramePool = EcsWorlds.EVENTS_WORLD;
        
        public void Run(EcsSystems systems)
        {
            EcsPool<TargetEntity> targetEntityPool = _filter.Pools.Inc1;
            EcsPool<AttackDamageValue> attackDamagePool = _filter.Pools.Inc2;
            EcsPool<DealDamageRequest> dealDamageRequestPool = _filter.Pools.Inc3;
            
            foreach (var entity in _filter.Value)
            {
                Entity targetEntity = targetEntityPool.Get(entity).Value;

                if (targetEntity == null)
                    return;
                    
                if(!_getDamageValuePool.Value.Has(targetEntity.Id))
                {
                    _getDamageValuePool.Value.Add(targetEntity.Id) =
                        new GetDamageValue { Value = attackDamagePool.Get(entity).Value };

                    int newEntity = _eventWorld.Value.NewEntity();
                    _isDamagedPool.Value.Add(newEntity) = new IsDamaged { DamagedEntity = targetEntity.Id };
                    _oneFramePool.Value.Add(newEntity);
                }
                dealDamageRequestPool.Del(entity);
            }
        }
    }
}