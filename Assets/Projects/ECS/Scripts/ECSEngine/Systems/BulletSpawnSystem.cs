using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace ECSHomework
{
    public sealed class BulletSpawnSystem : IEcsRunSystem
    {
        private readonly EcsWorldInject _eventWorld = EcsWorlds.EVENTS_WORLD;
        private readonly EcsFilterInject<Inc<SpawnRequest, Position, Rotation, Prefab>> _filter = EcsWorlds.EVENTS_WORLD;
        
        private EcsCustomInject<EntityManager> _entityManager;
        
        public void Run(EcsSystems systems)
        {
            EcsPool<SpawnRequest> spawnEventPool = _filter.Pools.Inc1;
            EcsPool<Position> positionPool = _filter.Pools.Inc2;
            EcsPool<Rotation> rotationPool = _filter.Pools.Inc3;
            EcsPool<Prefab> entityPrefabPool = _filter.Pools.Inc4;
            
            EntityManager entityManager = _entityManager.Value;

            foreach (int entity in _filter.Value)
            {
                
                Prefab bulletPrefab = entityPrefabPool.Get(entity);
                Position spawnPosition = positionPool.Get(entity);
                Rotation rotation = rotationPool.Get(entity);
                
                entityManager.CreateEntity(bulletPrefab.Value, spawnPosition.Value, rotation.Value);
                
                _eventWorld.Value.DelEntity(entity);
            }
        }
    }
}