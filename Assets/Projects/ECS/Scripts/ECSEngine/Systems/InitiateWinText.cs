using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace ECSHomework
{
    public class InitiateWinText : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<EntityObject, Inactive, IsBase>, Exc<GameOver>> _deadFilter;
        private readonly EcsPoolInject<TeamComponent> _teamComponentPool;
        
        private readonly EcsWorldInject _eventsWorld = EcsWorlds.EVENTS_WORLD;
        private readonly EcsPoolInject<CanDisplayWinText> _canDisplayTextPool = EcsWorlds.EVENTS_WORLD;
        private readonly EcsPoolInject<TeamComponent> _teamComponentPoolEventWorld = EcsWorlds.EVENTS_WORLD;
        
        
        public void Run(EcsSystems systems)
        {
            foreach (int entity in _deadFilter.Value)
            {
                int newEvent = _eventsWorld.Value.NewEntity();
                _canDisplayTextPool.Value.Add(newEvent);
                
                Teams team = _teamComponentPool.Value.Get(entity).Value;
                if (team == Teams.Blue)
                {
                    _teamComponentPoolEventWorld.Value.Add(newEvent) = new TeamComponent { Value = Teams.Red };
                }
                else
                {
                    _teamComponentPoolEventWorld.Value.Add(newEvent) = new TeamComponent { Value = Teams.Blue };
                }
            }
        }
    }
}