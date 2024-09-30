using ECSHomework.UnitManagerComponents;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace ECSHomework
{
    public sealed class SpawnUnitRequestSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<
            CanSpawn,
            TeamManagerComponent, 
            StartConfigComponent, 
            TeamComponent>> _filter;

        private readonly EcsWorldInject _eventWorld = EcsWorlds.EVENTS_WORLD;

        private readonly EcsPoolInject<SpawnRequest> _spawnRequestPool = EcsWorlds.EVENTS_WORLD;
        private readonly EcsPoolInject<TeamManagerComponent> _teamManagerPool = EcsWorlds.EVENTS_WORLD;
        private readonly EcsPoolInject<UnitTypeComponent> _unitTypePool = EcsWorlds.EVENTS_WORLD;
        private readonly EcsPoolInject<TeamComponent> _teamComponentPool = EcsWorlds.EVENTS_WORLD;
        private readonly EcsPoolInject<Position> _positionPool = EcsWorlds.EVENTS_WORLD;
        private readonly EcsPoolInject<Rotation> _rotationPool = EcsWorlds.EVENTS_WORLD;
        
        public void Run(EcsSystems systems)
        {
            EcsPool<CanSpawn> canSpawnPool = _filter.Pools.Inc1;
            EcsPool<TeamManagerComponent> teamManagerPool = _filter.Pools.Inc2;
            EcsPool<StartConfigComponent> teamConfigPool = _filter.Pools.Inc3;
            EcsPool<TeamComponent> teamPool = _filter.Pools.Inc4;

            foreach (int entity in _filter.Value)
            {
                bool canSpawn = canSpawnPool.Get(entity).Value;
                if (!canSpawn)
                    return;
                
                StartConfig startConfig = teamConfigPool.Get(entity).Value;
                Teams team = teamPool.Get(entity).Value;

                if (startConfig != null)
                {
                    foreach (var unit in startConfig.units)
                    {
                        if (unit.team == team && unit.spawnPoint != null)
                        {
                            int newEntity = _eventWorld.Value.NewEntity();
                            _spawnRequestPool.Value.Add(newEntity) = new SpawnRequest();
                            _teamManagerPool.Value.Add(newEntity) = new TeamManagerComponent {Value = teamManagerPool.Get(entity).Value};
                            _unitTypePool. Value.Add(newEntity) = new UnitTypeComponent {Value = unit.type};
                            _teamComponentPool.Value.Add(newEntity) = new TeamComponent {Value = team};
                            _positionPool.Value.Add(newEntity) = new Position {Value = unit.spawnPoint.position};
                            _rotationPool.Value.Add(newEntity) = new Rotation {Value = unit.spawnPoint.rotation};
                        }
                        else if(unit.spawnPoint == null)
                        {
                            Debug.LogError("Check SpawnPoints in Config.");
                        }
                    }
                }
                canSpawnPool.Del(entity);
            }
        }
    }
}