using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace ECSProject
{
    public sealed class GetDamageSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<IsDamaged>> _filter = EcsWorlds.EVENTS_WORLD;
        
        private readonly EcsPoolInject<Health> _healthPool;
        private readonly EcsPoolInject<GetDamageValue> _getDamageValuePool;
        
        public void Run(EcsSystems systems)
        {
            EcsPool<IsDamaged> isDamagedPool = _filter.Pools.Inc1;
            
            foreach (var entity in _filter.Value)
            {
                int damagedEntity = isDamagedPool.Get(entity).DamagedEntity;
                
                ref int targetHealth = ref _healthPool.Value.Get(damagedEntity).CurrentValue;
                int damageValue = _getDamageValuePool.Value.Get(damagedEntity).Value;
            
                targetHealth = Mathf.Max(0, targetHealth-damageValue);
                _getDamageValuePool.Value.Del(damagedEntity);
            }
        }
    }
}