using ECSHomework.VFX;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace ECSHomework
{
    public sealed class ApplyFireSound : IEcsRunSystem
    {
        private readonly EcsFilterInject<
            Inc<Health, IsBase>, Exc<Inactive>> _filter;
        
        private readonly EcsPoolInject<AudioSourceComponent> _audioSourcePool;
        private readonly EcsPoolInject<FireSFX> _fireSoundPool;
        public void Run(EcsSystems systems)
        {
            EcsPool<Health> healthPool = _filter.Pools.Inc1;

            foreach (var entity in _filter.Value)
            {

                int initialHealth = healthPool.Get(entity).InitialValue;
                int currentHealth = healthPool.Get(entity).CurrentValue;

                int leftHealthPercent = (currentHealth * 100) / initialHealth;

                if (leftHealthPercent != 100)
                {
                    AudioSource audioSource = _audioSourcePool.Value.Get(entity).Value;
                    AudioClip fireSound = _fireSoundPool.Value.Get(entity).Value;

                    if (leftHealthPercent is <= 70 and > 0 && !audioSource.isPlaying)
                    {
                        audioSource.PlayOneShot(fireSound);
                    }
                    else if(currentHealth == 0)
                    {
                        audioSource.Stop();
                    }
                }
            }
        }
    }
}
