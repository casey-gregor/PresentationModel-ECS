using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace ECSProject
{
    public sealed class ApplyBloodVFX : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<IsDamaged>> _filter = EcsWorlds.EVENTS_WORLD;
        
        private readonly EcsPoolInject<BloodVFX> _bloodVFXPool;
        
        public void Run(EcsSystems systems)
        {
            EcsPool<IsDamaged> isDamagedPool = _filter.Pools.Inc1;
            
            foreach (var entity in _filter.Value)
            {
                int damagedEntity = isDamagedPool.Get(entity).DamagedEntity;

                if (_bloodVFXPool.Value.Has(damagedEntity))
                {
                  
                    if (_bloodVFXPool.Value.Get(damagedEntity).Value.isStopped)
                    {
                        _bloodVFXPool.Value.Get(damagedEntity).Value.Play();
                    }
                }
            }
        }
    }
}