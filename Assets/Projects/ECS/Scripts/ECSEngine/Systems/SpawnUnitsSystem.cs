using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace ECSProject
{
    public sealed class SpawnUnitsSystem : IEcsRunSystem
    {
        private readonly EcsWorldInject _eventWorld = EcsWorlds.EVENTS_WORLD;
        
        private readonly EcsFilterInject
            <Inc<SpawnRequest, 
                TeamManagerComponent, 
                UnitTypeComponent, 
                PositionComponent, 
                RotationComponent,
                TeamComponent>> _filter = EcsWorlds.EVENTS_WORLD;

        private readonly EcsPoolInject<TargetEntity> _targetEntityPoolEventWorld = EcsWorlds.EVENTS_WORLD;
        private readonly EcsPoolInject<TargetEntity> _targetEntityPoolDefaultWorld;
  
        private readonly EcsPoolInject<UnitTypeComponent> _unitTypePool;
        private readonly EcsPoolInject<TeamComponent> _teamPool;
        private readonly EcsPoolInject<TeamManagerComponent> _teamManagerPool;
        private readonly EcsPoolInject<TrajectoryCalculateRequest> _trajectoryCalculateRequestPool;

        
        public void Run(EcsSystems systems)
        {
            EcsPool<TeamManagerComponent> teamManagerPool = _filter.Pools.Inc2;
            EcsPool<UnitTypeComponent> typePool = _filter.Pools.Inc3;
            EcsPool<PositionComponent> positionPool = _filter.Pools.Inc4;
            EcsPool<RotationComponent> rotationPool = _filter.Pools.Inc5;
            EcsPool<TeamComponent> teamComponentPool = _filter.Pools.Inc6;
            
            foreach (var spawnEvent in _filter.Value)
            {
                TeamManager teamManager = teamManagerPool.Get(spawnEvent).Value;
                UnitTypes type = typePool.Get(spawnEvent).Value;

                Teams team = teamComponentPool.Get(spawnEvent).Value;
                Vector3 position = positionPool.Get(spawnEvent).Value;
                Quaternion rotation = rotationPool.Get(spawnEvent).Value;

                var newEntity = CreateNewEntity(systems, teamManager, type, position, rotation);

                CheckAssignTarget(spawnEvent, newEntity);
                CheckInitializeProjectile(newEntity, team);
                
                _eventWorld.Value.DelEntity(spawnEvent);
            }
        }

        private int CreateNewEntity(
            EcsSystems systems, 
            TeamManager teamManager, 
            UnitTypes type, 
            Vector3 position,
            Quaternion rotation)
        {
            EcsWorld world = systems.GetWorld();
            int newEntity = world.NewEntity();
                
            Entity spawnedUnit = teamManager.SpawnUnit(
                type, 
                position, 
                rotation);

            if (spawnedUnit != null)
            {
                spawnedUnit.Initialize(newEntity, world);
            }
            
            _teamManagerPool.Value.Add(newEntity) = new TeamManagerComponent { Value = teamManager };
            return newEntity;
        }

        private void CheckInitializeProjectile(int newEntity, Teams team)
        {
            if (_unitTypePool.Value.Get(newEntity).Value == UnitTypes.Projectile)
            {
                _trajectoryCalculateRequestPool.Value.Add(newEntity);
                _teamPool.Value.Get(newEntity).Value = team;
            }
        }

        private void CheckAssignTarget(int entity, int newEntity)
        {
            if (_targetEntityPoolEventWorld.Value.Has(entity))
            {
                Entity target = _targetEntityPoolEventWorld.Value.Get(entity).Value;
                _targetEntityPoolDefaultWorld.Value.Get(newEntity).Value = target;
            }
        }
    }
    
   
}