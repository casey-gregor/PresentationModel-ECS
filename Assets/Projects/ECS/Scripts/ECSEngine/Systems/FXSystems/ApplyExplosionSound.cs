using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace ECSHomework
{
    public sealed class ApplyExplosionSound : IEcsRunSystem
    {
        private readonly EcsFilterInject<
            Inc<Health, IsBase>, Exc<Inactive>> _filter;

        private readonly EcsPoolInject<AudioSourceComponent> _audioSourcePool;
        private readonly EcsPoolInject<ExplodeSFX> _explodeSoundPool;

        public void Run(EcsSystems systems)
        {
            EcsPool<Health> healthPool = _filter.Pools.Inc1;

            foreach (var entity in _filter.Value)
            {

                int currentHealth = healthPool.Get(entity).CurrentValue;

                if (currentHealth == 0)
                {
                    AudioSource audioSource = _audioSourcePool.Value.Get(entity).Value;
                    AudioClip explodeSound = _explodeSoundPool.Value.Get(entity).Value;

                    audioSource.PlayOneShot(explodeSound);
                }
            }
        }
    }
}