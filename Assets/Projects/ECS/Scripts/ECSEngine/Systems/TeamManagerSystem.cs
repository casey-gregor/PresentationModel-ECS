using System.Collections.Generic;
using ECSProject.UnitManagerComponents;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace ECSProject
{
    public sealed class TeamManagerSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<CreateUnitPools, UnitConfigList, WorldParent, UnitsParent>> _filter;

        private readonly EcsPoolInject<TeamManagerComponent> _teamManagerPool;
        private readonly EcsPoolInject<CanSpawn> _canSpawnPool;
        
        public void Run(EcsSystems systems)
        {
            EcsPool<CreateUnitPools> createUnitsPool = _filter.Pools.Inc1;
            EcsPool<UnitConfigList> unitConfigListPool = _filter.Pools.Inc2;
            EcsPool<WorldParent> worldParentPool = _filter.Pools.Inc3;
            EcsPool<UnitsParent> unitsParentPool = _filter.Pools.Inc4;
            
            foreach (var entity in _filter.Value)
            {
                List<UnitConfig> unitConfigList = unitConfigListPool.Get(entity).Value;
                Transform worldParent = worldParentPool.Get(entity).Value;
                Transform unitsParent = unitsParentPool.Get(entity).Value;
                
                TeamManager teamManager = new TeamManager(unitConfigList, worldParent, unitsParent);
                
                _teamManagerPool.Value.Add(entity) = new TeamManagerComponent { Value = teamManager };
                _canSpawnPool.Value.Add(entity) = new CanSpawn { Value = true };
                
                createUnitsPool.Del(entity);
            }
        }
    }
}