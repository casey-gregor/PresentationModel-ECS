using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace ECSHomework
{
    public sealed class FireRequestSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<FireRequest, BulletWeapon>> _filter;

        private readonly EcsWorldInject _eventWorld = EcsWorlds.EVENTS_WORLD;

        private readonly EcsPoolInject<SpawnRequest> _spawnPool = EcsWorlds.EVENTS_WORLD;
        private readonly EcsPoolInject<Position> _positionPool = EcsWorlds.EVENTS_WORLD;
        private readonly EcsPoolInject<Rotation> _rotationPool = EcsWorlds.EVENTS_WORLD;
        private readonly EcsPoolInject<Prefab> _prefabPool = EcsWorlds.EVENTS_WORLD;

        public void Run(EcsSystems systems)
        {
            EcsPool<FireRequest> fireRequestPool = _filter.Pools.Inc1;
            EcsPool<BulletWeapon> weaponPool = _filter.Pools.Inc2;
            
            foreach (int entity in _filter.Value)
            {
                BulletWeapon bulletWeapon = weaponPool.Get(entity);
                
                int newEntity = _eventWorld.Value.NewEntity();
                
                _spawnPool.Value.Add(newEntity) = new SpawnRequest();
                _positionPool.Value.Add(newEntity) = new Position{ Value = bulletWeapon.Firepoint.position };
                _rotationPool.Value.Add(newEntity) = new Rotation{ Value = bulletWeapon.Firepoint.rotation };
                _prefabPool.Value.Add(newEntity) = new Prefab { Value = bulletWeapon.BulletPrefab };
                
                fireRequestPool.Del(entity);
            }
        }
    }
}