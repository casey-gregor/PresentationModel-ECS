using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Unity.VisualScripting;
using UnityEngine;

namespace ECSHomework
{
    public sealed class FireEventSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject
            <Inc<FireRequest, 
                ShootingWeapon, 
                TargetEntity, 
                TeamManagerComponent, 
                TeamComponent>> _filter;

        private readonly EcsWorldInject _eventWorld = EcsWorlds.EVENTS_WORLD;

        private readonly EcsPoolInject<SpawnRequest> _spawnRequestPool = EcsWorlds.EVENTS_WORLD;
        private readonly EcsPoolInject<TeamManagerComponent> _teamManagerPool = EcsWorlds.EVENTS_WORLD;
        private readonly EcsPoolInject<TeamComponent> _teamComponentPool = EcsWorlds.EVENTS_WORLD;
        private readonly EcsPoolInject<UnitTypeComponent> _unitTypePool = EcsWorlds.EVENTS_WORLD;
        private readonly EcsPoolInject<Position> _positionPool = EcsWorlds.EVENTS_WORLD;
        private readonly EcsPoolInject<Rotation> _rotationPool = EcsWorlds.EVENTS_WORLD;
        private readonly EcsPoolInject<TargetEntity> _targetEntityPool = EcsWorlds.EVENTS_WORLD;

        public void Run(EcsSystems systems)
        {
  
            EcsPool<FireRequest> fireRequestPool = _filter.Pools.Inc1;
            EcsPool<ShootingWeapon> weaponPool = _filter.Pools.Inc2;
            EcsPool<TargetEntity> targetPool = _filter.Pools.Inc3;
            EcsPool<TeamManagerComponent> teamManagerPool = _filter.Pools.Inc4;
            EcsPool<TeamComponent> teamPool = _filter.Pools.Inc5;
            
            foreach (int entity in _filter.Value)
            {
                ShootingWeapon shootingWeapon = weaponPool.Get(entity);
                TargetEntity targetEntity = targetPool.Get(entity);
                TeamManagerComponent teamManager = teamManagerPool.Get(entity);
                UnitTypes type = weaponPool.Get(entity).ProjectileType;
                Teams team = teamPool.Get(entity).Value;

                int newEventEntity = _eventWorld.Value.NewEntity();
                
                _spawnRequestPool.Value.Add(newEventEntity) = new SpawnRequest();
                _teamManagerPool.Value.Add(newEventEntity) = new TeamManagerComponent { Value = teamManager.Value };
                _teamComponentPool.Value.Add(newEventEntity) = new TeamComponent { Value = team };
                _unitTypePool.Value.Add(newEventEntity) = new UnitTypeComponent { Value = type };
                _positionPool.Value.Add(newEventEntity) = new Position{ Value = shootingWeapon.Firepoint.position };
                _rotationPool.Value.Add(newEventEntity) = new Rotation{ Value = shootingWeapon.Firepoint.rotation };
                _targetEntityPool.Value.Add(newEventEntity) = new TargetEntity() { Value = targetEntity.Value };
                
                fireRequestPool.Del(entity);
            }
        }
    }
}