using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace ECSProject
{
    public sealed class CheckGameOverSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<EntityObject, Inactive, IsBase>, Exc<GameOver>> _deadBaseFilter;
        
        private readonly EcsFilterInject<Inc<EntityObject>, Exc<DeathRequest, GameOver>> _aliveEntitiesFilter;
        
        private readonly EcsPoolInject<GameOver> _gameOverPool;
        private readonly EcsPoolInject<TeamComponent> _teamComponentPool;
        

        public void Run(EcsSystems systems)
        {
            if (!IsSingleTeamAlive())
            {
                return;
            }
            
            foreach (int entity in _aliveEntitiesFilter.Value)
            {
                _gameOverPool.Value.Add(entity);
            }
        }
        
        private bool IsSingleTeamAlive()
        {
            Teams teamFound = Teams.None;

            foreach (int entity in _aliveEntitiesFilter.Value)
            {
                Teams team = _teamComponentPool.Value.Get(entity).Value;

                if (teamFound == Teams.None)
                {
                    teamFound = team;
                }
                else if (teamFound != team)
                {
                    return false;
                }
            }
            return true;
        }
        
    }
}