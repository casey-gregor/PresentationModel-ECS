using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace ECSProject
{
    public sealed class ApplyWinSound : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<CanPlayWinSound>> _filterEvent = EcsWorlds.EVENTS_WORLD;

        private readonly EcsFilterInject<Inc<WinSFX>> _filterDefault;
        private readonly EcsPoolInject<AudioSourceComponent> _audioSourcePool;
        private readonly EcsPoolInject<WinSFX> _winSoundPool;

        public void Run(EcsSystems systems)
        {
            EcsPool<CanPlayWinSound> canPlaySoundPool = _filterEvent.Pools.Inc1;
            
            foreach (var _event in _filterEvent.Value)
            {
                foreach (var entity in _filterDefault.Value)
                {
                    AudioSource audioSource = _audioSourcePool.Value.Get(entity).Value;
                    AudioClip winSound = _winSoundPool.Value.Get(entity).Value;
                        
                    audioSource.PlayOneShot(winSound);
                }
                canPlaySoundPool.Del(_event);
                
            }
        }
    }
}