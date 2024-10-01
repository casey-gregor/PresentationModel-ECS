using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace ECSHomework
{
    public class ApplyWinSound : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<CanPlayWinSound, Timer>> _filterEvent = EcsWorlds.EVENTS_WORLD;

        private readonly EcsFilterInject<Inc<WinSFX>> _filterDefault;
        private readonly EcsPoolInject<AudioSourceComponent> _audioSourcePool;
        private readonly EcsPoolInject<WinSFX> _winSoundPool;

        private const float adjustDelay = 0.5f;
        public void Run(EcsSystems systems)
        {
            EcsPool<CanPlayWinSound> canPlaySoundPool = _filterEvent.Pools.Inc1;
            EcsPool<Timer> timeDelayPool = _filterEvent.Pools.Inc2;
            
            foreach (var _event in _filterEvent.Value)
            {
                // Debug.Log("try apply win sound");
                ref float timeDelay = ref timeDelayPool.Get(_event).Value;
                timeDelay -= adjustDelay;
                
                if (timeDelay <= 0)
                {
                    foreach (var entity in _filterDefault.Value)
                    {
                        AudioSource audioSource = _audioSourcePool.Value.Get(entity).Value;
                        AudioClip winSound = _winSoundPool.Value.Get(entity).Value;
                        
                        audioSource.PlayOneShot(winSound);
                    }
                    canPlaySoundPool.Del(_event);
                }
                
                timeDelay -= Time.deltaTime;
                
            }
        }
    }
}