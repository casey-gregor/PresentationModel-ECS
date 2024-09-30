using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace ECSHomework
{
    public sealed class ApplyHurtSound : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<IsDamaged>> _filter = EcsWorlds.EVENTS_WORLD;

        private readonly EcsPoolInject<AudioSourceComponent> _audioPool;
        private readonly EcsPoolInject<HurtSFX> _hurtSFXPool;
        
        public void Run(EcsSystems systems)
        {
            EcsPool<IsDamaged> isDamagedPool = _filter.Pools.Inc1;

            foreach (var entity in _filter.Value)
            {
                int damagedEntity = isDamagedPool.Get(entity).DamagedEntity;
                
                if (_audioPool.Value.Has(damagedEntity) && _hurtSFXPool.Value.Has(damagedEntity))
                {
                    AudioClip audio = _hurtSFXPool.Value.Get(damagedEntity).Value;
                    _audioPool.Value.Get(damagedEntity).Value.PlayOneShot(audio);
                }
            }
        }
    }
}