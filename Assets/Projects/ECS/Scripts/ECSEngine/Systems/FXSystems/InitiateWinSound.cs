using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace ECSHomework
{
    public class InitiateWinSound : IEcsRunSystem
    {
        private readonly EcsFilterInject<
            Inc<Health, IsBase, Timer>, Exc<Inactive>> _filter;
        
        private readonly EcsPoolInject<AudioSourceComponent> _audioSourcePool;

        private readonly EcsWorldInject _eventsWorld = EcsWorlds.EVENTS_WORLD;
        private readonly EcsPoolInject<CanPlayWinSound> _canPlaySoundPool = EcsWorlds.EVENTS_WORLD;
        private readonly EcsPoolInject<Timer> _timeDelayPool = EcsWorlds.EVENTS_WORLD;
        
        public void Run(EcsSystems systems)
        {
            EcsPool<Health> healthPool = _filter.Pools.Inc1;
            EcsPool<Timer> timeDelayPool = _filter.Pools.Inc3;
            
            foreach (var entity in _filter.Value)
            {
                int currentHealth = healthPool.Get(entity).CurrentValue;

                if (currentHealth == 0)
                {
                    AudioSource audioSource = _audioSourcePool.Value.Get(entity).Value;
                    float timeDelay = timeDelayPool.Get(entity).Value;
                    
                    int newEvent = _eventsWorld.Value.NewEntity();
                    _canPlaySoundPool.Value.Add(newEvent);
                    _timeDelayPool.Value.Add(newEvent) = new Timer { Value = timeDelay };
                }   
            }
        }
    }
}