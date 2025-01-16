using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace ECSProject
{
    public class InitiateWinSound : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<EntityObject, GameOver>, Exc<CanPlayWinSound>> _filter;
        
        private readonly EcsPoolInject<AudioSourceComponent> _audioSourcePool;
        private readonly EcsPoolInject<CanPlayWinSound> _canPlaySoundPool;
        
        private readonly EcsWorldInject _eventsWorld = EcsWorlds.EVENTS_WORLD;
        private readonly EcsPoolInject<CanPlayWinSound> _canPlaySoundPoolEvents = EcsWorlds.EVENTS_WORLD;
        
        public void Run(EcsSystems systems)
        {
            if (_filter.Value.GetEntitiesCount() > 0)
            {
                int newEvent = _eventsWorld.Value.NewEntity();
                _canPlaySoundPoolEvents.Value.Add(newEvent);
                
                foreach (var entity in _filter.Value)
                {
                    _canPlaySoundPool.Value.Add(entity);
                }
            }
            
        }
    }
}